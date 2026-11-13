using System.ComponentModel.DataAnnotations;

namespace SistemaCompras.Application.Dtos;

public class ActualizarCompraDto
{
    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(200, MinimumLength = 3,
        ErrorMessage = "La descripción debe tener entre 3 y 200 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999",
        ErrorMessage = "El monto debe ser mayor que cero.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Fecha { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "Debe seleccionar un proveedor válido.")]
    public int ProveedorId { get; set; }
}