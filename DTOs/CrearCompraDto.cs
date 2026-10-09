namespace SistemaComprasAPI.DTOs;

public class CrearCompraDto
{
    public string Descripcion { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public int ProveedorId { get; set; }
}
