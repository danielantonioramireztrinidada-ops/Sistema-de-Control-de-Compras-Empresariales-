
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SistemaCompras.Application.Contract;
using SistemaCompras.Application.Dtos;

namespace SistemaComprasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly IProveedorService _proveedorService;

    public ProveedoresController(IProveedorService proveedorService)
    {
        _proveedorService = proveedorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProveedores()
    {
        var proveedores = await _proveedorService.GetAllAsync();

        return Ok(proveedores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProveedor(int id)
    {
        try
        {
            var proveedor = await _proveedorService.GetByIdAsync(id);

            if (proveedor == null)
                return NotFound();

            return Ok(proveedor);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CrearProveedor(CrearProveedorDto dto)
    {
        try
        {
            var proveedor = await _proveedorService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetProveedor),
                new { id = proveedor.Id },
                proveedor);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProveedor(
        int id,
        ActualizarProveedorDto dto)
    {
        try
        {
            var actualizado = await _proveedorService.UpdateAsync(id, dto);

            if (!actualizado)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarProveedor(int id)
    {
        try
        {
            var eliminado = await _proveedorService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
