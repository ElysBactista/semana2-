using GestionSolicitudes.Application.DTOs;

namespace GestionSolicitudes.Application.Interfaces;

public interface ISolicitudService
{
    Task<(int Total, IEnumerable<SolicitudDto> Solicitudes)> ObtenerTodasAsync(string? busqueda, int numeroPagina, int tamanoPagina); Task<SolicitudDto?> ObtenerPorIdAsync(int id);
    Task<SolicitudDto> CrearAsync(CrearSolicitudDto dto);
    Task<bool> ActualizarAsync(int id, ActualizarSolicitudDto dto);      // PUT
    Task<bool> CambiarEstadoAsync(int id, ActualizarEstadoDto dto);   // PATCH
    Task<bool> DesactivarAsync(int id);                              // DELETE
}