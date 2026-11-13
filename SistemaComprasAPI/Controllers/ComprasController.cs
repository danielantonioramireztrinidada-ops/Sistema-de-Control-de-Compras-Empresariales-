
using Microsoft.AspNetCore.Mvc;
using SistemaCompras.Domain.Entities;
using SistemaCompras.Infrastructure.Interfaces;
using SistemaCompras.Infrastructure.Models;

namespace SistemaComprasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComprasController : ControllerBase
{
    private readonly ICompraRepository _compraRepository;

    public ComprasController(ICompraRepository compraRepository)
    {
        _compraRepository = compraRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompras()
    {
        var compras = await _compraRepository.GetAllWithProveedorAsync();

        return Ok(compras.Select(c => new CompraDto
        {
            Id = c.Id,
            Descripcion = c.Descripcion,
            Monto = c.Monto,
            Fecha = c.Fecha,
            ProveedorId = c.ProveedorId,
            NombreProveedor = c.Proveedor!.Nombre
        }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompra(int id)
    {
        var c = await _compraRepository.GetByIdWithProveedorAsync(id);

        if (c == null)
            return NotFound();

        return Ok(new CompraDto
        {
            Id = c.Id,
            Descripcion = c.Descripcion,
            Monto = c.Monto,
            Fecha = c.Fecha,
            ProveedorId = c.ProveedorId,
            NombreProveedor = c.Proveedor!.Nombre
        });
    }

    [HttpPost]
    public async Task<IActionResult> CrearCompra(CrearCompraDto dto)
    {
        if (dto.Monto <= 0)
            return BadRequest("El monto debe ser mayor que cero.");

        var proveedorExiste = await _compraRepository
            .ProveedorExistsAsync(dto.ProveedorId);

        if (!proveedorExiste)
            return BadRequest("El proveedor indicado no existe.");

        var compra = new Compra
        {
            Descripcion = dto.Descripcion,
            Monto = dto.Monto,
            Fecha = dto.Fecha,
            ProveedorId = dto.ProveedorId
        };

        await _compraRepository.AddAsync(compra);

        var compraCreada = await _compraRepository
            .GetByIdWithProveedorAsync(compra.Id);

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
                NombreProveedor = compraCreada!.Proveedor!.Nombre
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarCompra(
        int id, ActualizarCompraDto dto)
    {
        var compra = await _compraRepository.GetByIdAsync(id);

        if (compra == null)
            return NotFound();

        if (dto.Monto <= 0)
            return BadRequest("El monto debe ser mayor que cero.");

        var proveedorExiste = await _compraRepository
            .ProveedorExistsAsync(dto.ProveedorId);

        if (!proveedorExiste)
            return BadRequest("El proveedor indicado no existe.");

        compra.Descripcion = dto.Descripcion;
        compra.Monto = dto.Monto;
        compra.Fecha = dto.Fecha;
        compra.ProveedorId = dto.ProveedorId;

        await _compraRepository.UpdateAsync(compra);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCompra(int id)
    {
        var compra = await _compraRepository.GetByIdAsync(id);

        if (compra == null)
            return NotFound();

        await _compraRepository.DeleteAsync(compra);

        return NoContent();
    }
}
