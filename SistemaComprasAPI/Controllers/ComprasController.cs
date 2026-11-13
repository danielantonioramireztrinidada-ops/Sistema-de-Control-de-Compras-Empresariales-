
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SistemaCompras.Application.Contract;
using SistemaCompras.Application.Dtos;

namespace SistemaComprasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComprasController : ControllerBase
{
    private readonly ICompraService _compraService;

    public ComprasController(ICompraService compraService)
    {
        _compraService = compraService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompras()
    {
        var compras = await _compraService.GetAllAsync();

        return Ok(compras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompra(int id)
    {
        try
        {
            var compra = await _compraService.GetByIdAsync(id);

            if (compra == null)
                return NotFound();

            return Ok(compra);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CrearCompra(CrearCompraDto dto)
    {
        try
        {
            var compra = await _compraService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetCompra),
                new { id = compra.Id },
                compra);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarCompra(
        int id,
        ActualizarCompraDto dto)
    {
        try
        {
            var actualizado = await _compraService.UpdateAsync(id, dto);

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
    public async Task<IActionResult> EliminarCompra(int id)
    {
        try
        {
            var eliminado = await _compraService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
