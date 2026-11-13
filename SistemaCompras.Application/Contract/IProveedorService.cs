using SistemaCompras.Application.Dtos;

namespace SistemaCompras.Application.Contract;

public interface IProveedorService
{
    Task<List<ProveedorDto>> GetAllAsync();

    Task<ProveedorDto?> GetByIdAsync(int id);

    Task<ProveedorDto> CreateAsync(CrearProveedorDto dto);

    Task<bool> UpdateAsync(int id, ActualizarProveedorDto dto);

    Task<bool> DeleteAsync(int id);
}