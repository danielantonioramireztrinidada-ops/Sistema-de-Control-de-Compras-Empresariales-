
using SistemaCompras.Domain.Core;

namespace SistemaCompras.Domain.Entities;

public class Proveedor : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
