using GestionSolicitudes.Domain;

namespace GestionSolicitudes.Application.DTOs;

public class SolicitudDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Solicitante { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }

}