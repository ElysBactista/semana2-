using GestionSolicitudes.Application.DTOs;
using GestionSolicitudes.Application.Interfaces;
using GestionSolicitudes.Domain;
using GestionSolicitudes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestionSolicitudes.Infrastructure;

public class SolicitudService : ISolicitudService
{
    private readonly ApplicationDbContext _context;

    public SolicitudService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SolicitudDto>> ObtenerTodasAsync()
    {
        return await _context.Solicitudes
            .Where(s => s.Activo)
            .Select(s => MapToDto(s))
            .ToListAsync();
    }

    public async Task<SolicitudDto?> ObtenerPorIdAsync(int id)
    {
        var solicitud = await _context.Solicitudes
            .FirstOrDefaultAsync(s => s.Id == id && s.Activo);

        return solicitud != null ? MapToDto(solicitud) : null;
    }

    public async Task<SolicitudDto> CrearAsync(CrearSolicitudDto dto)
    {
        var nuevaSolicitud = new Solicitud
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Solicitante = dto.Solicitante,
            Estado = EstadoSolicitud.Pendiente,
            FechaCreacion = DateTime.UtcNow,
            Activo = true
        };

        _context.Solicitudes.Add(nuevaSolicitud);
        await _context.SaveChangesAsync();

        return MapToDto(nuevaSolicitud);
    }

    // --- PUT: Editar datos generales ---
    public async Task<bool> ActualizarAsync(int id, ActualizarSolicitudDto dto)
    {
        var solicitud = await _context.Solicitudes
            .FirstOrDefaultAsync(s => s.Id == id && s.Activo);

        if (solicitud == null) return false;

        solicitud.Titulo = dto.Titulo;
        solicitud.Descripcion = dto.Descripcion ?? string.Empty;
        solicitud.Solicitante = dto.Solicitante;
        solicitud.FechaActualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    // --- PATCH: Cambiar estado ---
    public async Task<bool> CambiarEstadoAsync(int id, ActualizarEstadoDto dto)
    {
        var solicitud = await _context.Solicitudes
            .FirstOrDefaultAsync(s => s.Id == id && s.Activo);

        if (solicitud == null) return false;

        solicitud.Estado = dto.NuevoEstado;
        solicitud.FechaActualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    // --- DELETE: Desactivar registro ---
    public async Task<bool> DesactivarAsync(int id)
    {
        var solicitud = await _context.Solicitudes
            .FirstOrDefaultAsync(s => s.Id == id && s.Activo);

        if (solicitud == null) return false;

        solicitud.Activo = false;
        solicitud.FechaActualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    private static SolicitudDto MapToDto(Solicitud s)
    {
        return new SolicitudDto
        {
            Id = s.Id,
            Titulo = s.Titulo,
            Descripcion = s.Descripcion,
            Solicitante = s.Solicitante,
            Estado = s.Estado.ToString(),
            FechaCreacion = s.FechaCreacion
        };
    }
}