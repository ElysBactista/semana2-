using System;
using GestionSolicitudes.Domain;

namespace GestionSolicitudes.Application.DTOs;

public class SolicitudDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Solicitante { get; set; } = string.Empty;
    public EstadoSolicitud Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
}