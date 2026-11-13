
using Microsoft.EntityFrameworkCore;
using SistemaCompras.Domain.Entities;

namespace SistemaCompras.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Proveedor> Proveedores => Set<Proveedor>();

    public DbSet<Compra> Compras => Set<Compra>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Proveedor>()
            .HasMany(p => p.Compras)
            .WithOne(c => c.Proveedor)
            .HasForeignKey(c => c.ProveedorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
