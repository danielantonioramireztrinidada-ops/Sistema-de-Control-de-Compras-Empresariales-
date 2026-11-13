using System.ComponentModel.DataAnnotations;

namespace SistemaCompras.Application.Dtos;

public class ActualizarProveedorDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
    [StringLength(20, MinimumLength = 7,
        ErrorMessage = "El teléfono debe tener entre 7 y 20 caracteres.")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    [StringLength(100,
        ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    public string Correo { get; set; } = string.Empty;
}