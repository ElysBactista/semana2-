using GestionSolicitudes.Application.DTOs;

namespace GestionSolicitudes.Application.Interfaces;

public interface ISolicitudService
{
    Task<IEnumerable<SolicitudDto>> ObtenerTodasAsync();
    Task<SolicitudDto?> ObtenerPorIdAsync(int id);
    Task<SolicitudDto> CrearAsync(CrearSolicitudDto dto);
    Task<bool> ActualizarAsync(int id, ActualizarSolicitudDto dto);      // PUT
    Task<bool> CambiarEstadoAsync(int id, ActualizarEstadoDto dto);   // PATCH
    Task<bool> DesactivarAsync(int id);                              // DELETE
}