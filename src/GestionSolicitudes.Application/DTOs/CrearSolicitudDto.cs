using System.ComponentModel.DataAnnotations;

namespace GestionSolicitudes.Application.DTOs;

public class CrearSolicitudDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(100, ErrorMessage = "El título no puede exceder 100 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre del solicitante es obligatorio.")]
    public string Solicitante { get; set; } = string.Empty;
}