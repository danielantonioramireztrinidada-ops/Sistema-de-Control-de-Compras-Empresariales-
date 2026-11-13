
using Microsoft.EntityFrameworkCore;
using SistemaCompras.Domain.Entities;
using SistemaCompras.Infrastructure.Context;
using SistemaCompras.Infrastructure.Core;
using SistemaCompras.Infrastructure.Interfaces;

namespace SistemaCompras.Infrastructure.Repositories;

public class CompraRepository
    : BaseRepository<Compra>, ICompraRepository
{
    public CompraRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<Compra>> GetAllWithProveedorAsync()
    {
        return await DbSet
            .Include(c => c.Proveedor)
            .ToListAsync();
    }

    public async Task<Compra?> GetByIdWithProveedorAsync(int id)
    {
        return await DbSet
            .Include(c => c.Proveedor)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> ProveedorExistsAsync(int proveedorId)
    {
        return await Context.Proveedores
            .AnyAsync(p => p.Id == proveedorId);
    }
}
