using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaComprasAPI.Data;
using SistemaComprasAPI.DTOs;
using SistemaComprasAPI.Models;

namespace SistemaComprasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProveedoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProveedores()
    {
        var proveedores = await _context.Proveedores.ToListAsync();
        return Ok(proveedores.Select(p => new ProveedorDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Telefono = p.Telefono,
            Correo = p.Correo
        }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProveedor(int id)
    {
        var p = await _context.Proveedores.FindAsync(id);

        if (p == null)
            return NotFound();

        return Ok(new ProveedorDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Telefono = p.Telefono,
            Correo = p.Correo
        });
    }

    [HttpPost]
    public async Task<IActionResult> CrearProveedor(CrearProveedorDto dto)
    {
        var p = new Proveedor
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Correo = dto.Correo
        };

        _context.Proveedores.Add(p);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProveedor), new { id = p.Id },
            new ProveedorDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Correo = p.Correo
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProveedor(
        int id, ActualizarProveedorDto dto)
    {
        var p = await _context.Proveedores.FindAsync(id);

        if (p == null)
            return NotFound();

        p.Nombre = dto.Nombre;
        p.Telefono = dto.Telefono;
        p.Correo = dto.Correo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarProveedor(int id)
    {
        var p = await _context.Proveedores.FindAsync(id);

        if (p == null)
            return NotFound();

        if (await _context.Compras.AnyAsync(c => c.ProveedorId == id))
            return Conflict("El proveedor tiene compras registradas.");

        _context.Proveedores.Remove(p);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
