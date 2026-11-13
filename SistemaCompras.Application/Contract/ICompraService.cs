using SistemaCompras.Application.Dtos;

namespace SistemaCompras.Application.Contract;

public interface ICompraService
{
    Task<List<CompraDto>> GetAllAsync();

    Task<CompraDto?> GetByIdAsync(int id);

    Task<CompraDto> CreateAsync(CrearCompraDto dto);

    Task<bool> UpdateAsync(int id, ActualizarCompraDto dto);

    Task<bool> DeleteAsync(int id);
}