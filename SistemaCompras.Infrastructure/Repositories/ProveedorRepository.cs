
using Microsoft.EntityFrameworkCore;
using SistemaCompras.Domain.Entities;
using SistemaCompras.Infrastructure.Context;
using SistemaCompras.Infrastructure.Core;
using SistemaCompras.Infrastructure.Interfaces;

namespace SistemaCompras.Infrastructure.Repositories;

public class ProveedorRepository
    : BaseRepository<Proveedor>, IProveedorRepository
{
    public ProveedorRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<bool> HasComprasAsync(int proveedorId)
    {
        return await Context.Compras
            .AnyAsync(c => c.ProveedorId == proveedorId);
    }
}
