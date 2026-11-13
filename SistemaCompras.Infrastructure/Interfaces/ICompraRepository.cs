
using SistemaCompras.Domain.Entities;

namespace SistemaCompras.Infrastructure.Interfaces;

public interface ICompraRepository : IBaseRepository<Compra>
{
    Task<List<Compra>> GetAllWithProveedorAsync();

    Task<Compra?> GetByIdWithProveedorAsync(int id);

    Task<bool> ProveedorExistsAsync(int proveedorId);
}