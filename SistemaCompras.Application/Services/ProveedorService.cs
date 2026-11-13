
using System.ComponentModel.DataAnnotations;
using SistemaCompras.Application.Contract;
using SistemaCompras.Application.Dtos;
using SistemaCompras.Domain.Entities;
using SistemaCompras.Infrastructure.Interfaces;

namespace SistemaCompras.Application.Services;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepository _proveedorRepository;

    public ProveedorService(IProveedorRepository proveedorRepository)
    {
        _proveedorRepository = proveedorRepository;
    }

    public async Task<List<ProveedorDto>> GetAllAsync()
    {
        var proveedores = await _proveedorRepository.GetAllAsync();

        return proveedores.Select(p => MapToDto(p)).ToList();
    }

    public async Task<ProveedorDto?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID del proveedor no es válido.");

        var proveedor = await _proveedorRepository.GetByIdAsync(id);

        if (proveedor == null)
            return null;

        return MapToDto(proveedor);
    }

    public async Task<ProveedorDto> CreateAsync(CrearProveedorDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        dto.Nombre = dto.Nombre?.Trim() ?? string.Empty;
        dto.Telefono = dto.Telefono?.Trim() ?? string.Empty;
        dto.Correo = dto.Correo?.Trim() ?? string.Empty;

        Validate(dto);

        var proveedor = new Proveedor
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Correo = dto.Correo
        };

        var creado = await _proveedorRepository.AddAsync(proveedor);

        return MapToDto(creado);
    }

    public async Task<bool> UpdateAsync(
        int id,
        ActualizarProveedorDto dto)
    {
        if (id <= 0)
            throw new ArgumentException("El ID del proveedor no es válido.");

        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        dto.Nombre = dto.Nombre?.Trim() ?? string.Empty;
        dto.Telefono = dto.Telefono?.Trim() ?? string.Empty;
        dto.Correo = dto.Correo?.Trim() ?? string.Empty;

        Validate(dto);

        var proveedor = await _proveedorRepository.GetByIdAsync(id);

        if (proveedor == null)
            return false;

        proveedor.Nombre = dto.Nombre;
        proveedor.Telefono = dto.Telefono;
        proveedor.Correo = dto.Correo;

        await _proveedorRepository.UpdateAsync(proveedor);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID del proveedor no es válido.");

        var proveedor = await _proveedorRepository.GetByIdAsync(id);

        if (proveedor == null)
            return false;

        if (await _proveedorRepository.HasComprasAsync(id))
            throw new InvalidOperationException(
                "No se puede eliminar el proveedor porque tiene compras registradas.");

        await _proveedorRepository.DeleteAsync(proveedor);

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

    private static ProveedorDto MapToDto(Proveedor proveedor)
    {
        return new ProveedorDto
        {
            Id = proveedor.Id,
            Nombre = proveedor.Nombre,
            Telefono = proveedor.Telefono,
            Correo = proveedor.Correo
        };
    }
}
