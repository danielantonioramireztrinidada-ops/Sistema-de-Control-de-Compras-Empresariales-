using Microsoft.EntityFrameworkCore;
using SistemaComprasAPI.Models;

namespace SistemaComprasAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Proveedor> Proveedores => Set<Proveedor>();

    public DbSet<Compra> Compras => Set<Compra>();
}
