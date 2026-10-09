using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaComprasAPI.Data;
using SistemaComprasAPI.DTOs;
using SistemaComprasAPI.Models;

namespace SistemaComprasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComprasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ComprasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompras()
    {
        var compras = await _context.Compras
            .Select(c => new CompraDto
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
                Monto = c.Monto,
                Fecha = c.Fecha,
                ProveedorId = c.ProveedorId,
                NombreProveedor = c.Proveedor!.Nombre
            })
            .ToListAsync();

        return Ok(compras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompra(int id)
    {
        var c = await _context.Compras
            .Where(c => c.Id == id)
            .Select(c => new CompraDto
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
                Monto = c.Monto,
                Fecha = c.Fecha,
                ProveedorId = c.ProveedorId,
                NombreProveedor = c.Proveedor!.Nombre
            })
            .FirstOrDefaultAsync();

        if (c == null)
            return NotFound();

        return Ok(c);
    }

    [HttpPost]
    public async Task<IActionResult> CrearCompra(CrearCompraDto dto)
    {
        if (dto.Monto <= 0)
            return BadRequest("El monto debe ser mayor que cero.");

        var proveedor = await _context.Proveedores
            .FindAsync(dto.ProveedorId);

        if (proveedor == null)
            return BadRequest("El proveedor indicado no existe.");

        var compra = new Compra
        {
            Descripcion = dto.Descripcion,
            Monto = dto.Monto,
            Fecha = dto.Fecha,
            ProveedorId = dto.ProveedorId
        };

        _context.Compras.Add(compra);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCompra),
            new { id = compra.Id },
            new CompraDto
            {
                Id = compra.Id,
                Descripcion = compra.Descripcion,
                Monto = compra.Monto,
                Fecha = compra.Fecha,
                ProveedorId = compra.ProveedorId,
                NombreProveedor = proveedor.Nombre
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarCompra(
        int id, ActualizarCompraDto dto)
    {
        var compra = await _context.Compras.FindAsync(id);

        if (compra == null)
            return NotFound();

        if (dto.Monto <= 0)
            return BadRequest("El monto debe ser mayor que cero.");

        var proveedor = await _context.Proveedores
            .FindAsync(dto.ProveedorId);

        if (proveedor == null)
            return BadRequest("El proveedor indicado no existe.");

        compra.Descripcion = dto.Descripcion;
        compra.Monto = dto.Monto;
        compra.Fecha = dto.Fecha;
        compra.ProveedorId = dto.ProveedorId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCompra(int id)
    {
        var compra = await _context.Compras.FindAsync(id);

        if (compra == null)
            return NotFound();

        _context.Compras.Remove(compra);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
