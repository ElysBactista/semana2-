using GestionSolicitudes.Domain;

namespace GestionSolicitudes.Application.DTOs;

public class ActualizarEstadoDto
{
    public EstadoSolicitud NuevoEstado { get; set; }
}