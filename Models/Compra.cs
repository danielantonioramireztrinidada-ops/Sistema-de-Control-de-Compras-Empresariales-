
namespace SistemaComprasAPI.Models;

public class Compra
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = string.Empty;

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public int ProveedorId { get; set; }

    public Proveedor? Proveedor { get; set; }
}
