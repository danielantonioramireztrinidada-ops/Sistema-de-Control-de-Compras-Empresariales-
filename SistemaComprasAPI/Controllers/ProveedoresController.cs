
using Microsoft.AspNetCore.Mvc;
using SistemaCompras.Domain.Entities;
using SistemaCompras.Infrastructure.Interfaces;
using SistemaCompras.Infrastructure.Models;

namespace SistemaComprasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly IProveedorRepository _proveedorRepository;

    public ProveedoresController(
        IProveedorRepository proveedorRepository)
    {
        _proveedorRepository = proveedorRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProveedores()
    {
        var proveedores = await _proveedorRepository.GetAllAsync();

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
        var p = await _proveedorRepository.GetByIdAsync(id);

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
    public async Task<IActionResult> CrearProveedor(
        CrearProveedorDto dto)
    {
        var p = new Proveedor
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Correo = dto.Correo
        };

        await _proveedorRepository.AddAsync(p);

        return CreatedAtAction(
            nameof(GetProveedor),
            new { id = p.Id },
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
        int id,
        ActualizarProveedorDto dto)
    {
        var p = await _proveedorRepository.GetByIdAsync(id);

        if (p == null)
            return NotFound();

        p.Nombre = dto.Nombre;
        p.Telefono = dto.Telefono;
        p.Correo = dto.Correo;

        await _proveedorRepository.UpdateAsync(p);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarProveedor(int id)
    {
        var p = await _proveedorRepository.GetByIdAsync(id);

        if (p == null)
            return NotFound();

        if (await _proveedorRepository.HasComprasAsync(id))
            return Conflict("El proveedor tiene compras registradas.");

        await _proveedorRepository.DeleteAsync(p);

        return NoContent();
    }
}
