
using SistemaCompras.Domain.Core;

namespace SistemaCompras.Domain.Entities;

public class Compra : BaseEntity
{
    public string Descripcion { get; set; } = string.Empty;

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public int ProveedorId { get; set; }

    public Proveedor? Proveedor { get; set; }
}
