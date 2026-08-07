using System.ComponentModel.DataAnnotations;

namespace GestionSolicitudes.Application.DTOs;

public class ActualizarSolicitudDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El solicitante es obligatorio.")]
    public string Solicitante { get; set; } = string.Empty;
}