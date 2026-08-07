using GestionSolicitudes.Application.DTOs;
using GestionSolicitudes.Application.Interfaces;
using GestionSolicitudes.Domain;
using GestionSolicitudes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace GestionSolicitudes.Infrastructure.Services;
public class SolicitudService : ISolicitudService
{
    private readonly ApplicationDbContext _context;

    public SolicitudService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(int Total, IEnumerable<SolicitudDto> Solicitudes)> ObtenerTodasAsync(string? busqueda, int numeroPagina, int tamanoPagina)
    {
        // 1. Iniciamos la consulta
        var query = _context.Solicitudes.AsQueryable();

        // 2. Aplicamos el filtro de búsqueda si el usuario envió algo
        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            query = query.Where(s => s.Titulo.Contains(busqueda) || s.Solicitante.Contains(busqueda));
        }

        // 3. Contamos cuántos registros hay en total con ese filtro
        var totalRegistros = await query.CountAsync();

        // 4. Aplicamos la paginación (Skip y Take)
        var solicitudesPaginadas = await query
            .Skip((numeroPagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync();

        // 5. Mapeamos a DTO (Adapta estas propiedades según las que tengas en tu SolicitudDto)
        // 5. Mapeamos a DTO reutilizando tu método MapToDto que ya tienes abajo
        var listaDtos = solicitudesPaginadas.Select(MapToDto).ToList();
        // 6. Devolvemos el total y la lista
        return (totalRegistros, listaDtos);
    }

    public async Task<SolicitudDto?> ObtenerPorIdAsync(int id)
    {
        var solicitud = await _context.Solicitudes
            .FirstOrDefaultAsync(s => s.Id == id && s.Activo);

        return solicitud != null ? MapToDto(solicitud) : null;
    }

    public Task<SolicitudDto> CrearAsync(CrearSolicitudDto dto)
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
        _context.SaveChanges();

        return Task.FromResult(MapToDto(nuevaSolicitud));
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

        _context.SaveChanges();
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

        _context.SaveChanges();
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

        _context.SaveChanges();
        return true;
    }

    private static SolicitudDto MapToDto(Solicitud s) => new()
    {
        Id = s.Id,
        Titulo = s.Titulo,
        Descripcion = s.Descripcion,
        Solicitante = s.Solicitante,
        Estado = s.Estado,
        FechaCreacion = s.FechaCreacion
    };
}