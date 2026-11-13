
using SistemaCompras.Domain.Entities;

namespace SistemaCompras.Infrastructure.Interfaces;

public interface IProveedorRepository : IBaseRepository<Proveedor>
{
    Task<bool> HasComprasAsync(int proveedorId);
}
