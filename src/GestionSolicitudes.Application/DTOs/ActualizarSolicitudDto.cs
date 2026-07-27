namespace GestionSolicitudes.Application.DTOs;

public class ActualizarSolicitudDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Solicitante { get; set; } = string.Empty;
}