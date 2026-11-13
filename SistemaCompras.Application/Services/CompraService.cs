
using System.ComponentModel.DataAnnotations;
using SistemaCompras.Application.Contract;
using SistemaCompras.Application.Dtos;
using SistemaCompras.Domain.Entities;
using SistemaCompras.Infrastructure.Interfaces;

namespace SistemaCompras.Application.Services;

public class CompraService : ICompraService
{
    private readonly ICompraRepository _compraRepository;
    private readonly IProveedorRepository _proveedorRepository;

    public CompraService(
        ICompraRepository compraRepository,
        IProveedorRepository proveedorRepository)
    {
        _compraRepository = compraRepository;
        _proveedorRepository = proveedorRepository;
    }

    public async Task<List<CompraDto>> GetAllAsync()
    {
        var compras = await _compraRepository.GetAllWithProveedorAsync();

        return compras.Select(MapToDto).ToList();
    }

    public async Task<CompraDto?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la compra no es válido.");

        var compra = await _compraRepository.GetByIdWithProveedorAsync(id);

        if (compra == null)
            return null;

        return MapToDto(compra);
    }

    public async Task<CompraDto> CreateAsync(CrearCompraDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        dto.Descripcion = dto.Descripcion?.Trim() ?? string.Empty;

        Validate(dto);

        if (dto.Fecha == DateTime.MinValue)
            throw new ValidationException("La fecha de la compra es obligatoria.");

        var proveedor = await _proveedorRepository.GetByIdAsync(dto.ProveedorId);

        if (proveedor == null)
            throw new ValidationException("El proveedor indicado no existe.");

        var compra = new Compra
        {
            Descripcion = dto.Descripcion,
            Monto = dto.Monto,
            Fecha = dto.Fecha,
            ProveedorId = dto.ProveedorId
        };

        var creada = await _compraRepository.AddAsync(compra);

        creada.Proveedor = proveedor;

        return MapToDto(creada);
    }

    public async Task<bool> UpdateAsync(
        int id,
        ActualizarCompraDto dto)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la compra no es válido.");

        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        dto.Descripcion = dto.Descripcion?.Trim() ?? string.Empty;

        Validate(dto);

        if (dto.Fecha == DateTime.MinValue)
            throw new ValidationException("La fecha de la compra es obligatoria.");

        var compra = await _compraRepository.GetByIdAsync(id);

        if (compra == null)
            return false;

        var proveedor = await _proveedorRepository.GetByIdAsync(dto.ProveedorId);

        if (proveedor == null)
            throw new ValidationException("El proveedor indicado no existe.");

        compra.Descripcion = dto.Descripcion;
        compra.Monto = dto.Monto;
        compra.Fecha = dto.Fecha;
        compra.ProveedorId = dto.ProveedorId;

        await _compraRepository.UpdateAsync(compra);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la compra no es válido.");

        var compra = await _compraRepository.GetByIdAsync(id);

        if (compra == null)
            return false;

        await _compraRepository.DeleteAsync(compra);

        return true;
    }

    private static void Validate(object dto)
    {
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            dto,
            context,
            results,
            validateAllProperties: true);

        if (!isValid)
        {
            var errors = string.Join(
                " ",
                results.Select(r => r.ErrorMessage));

            throw new ValidationException(errors);
        }
    }

    private static CompraDto MapToDto(Compra compra)
    {
        return new CompraDto
        {
            Id = compra.Id,
            Descripcion = compra.Descripcion,
            Monto = compra.Monto,
            Fecha = compra.Fecha,
            ProveedorId = compra.ProveedorId,
            NombreProveedor = compra.Proveedor?.Nombre ?? string.Empty
        };
    }
}
