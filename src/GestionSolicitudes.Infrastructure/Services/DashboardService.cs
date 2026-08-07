using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionSolicitudes.Domain;
using GestionSolicitudes.Application.DTOs;
using GestionSolicitudes.Application.Interfaces;
using GestionSolicitudes.Infrastructure.Persistence;

namespace GestionSolicitudes.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardResumenDto> ObtenerResumenAsync(DateTime? fechaInicio, DateTime? fechaFin)
    {
        var query = _context.Solicitudes.AsQueryable();

        if (fechaInicio.HasValue)
        {
            query = query.Where(s => s.FechaCreacion >= fechaInicio.Value);
        }

        if (fechaFin.HasValue)
        {
            query = query.Where(s => s.FechaCreacion <= fechaFin.Value);
        }

        var total = await query.CountAsync();
        var completados = await query.CountAsync(s => s.Estado == EstadoSolicitud.Aprobada);
        var pendientes = await query.CountAsync(s => s.Estado == EstadoSolicitud.Pendiente);
        var rechazados = await query.CountAsync(s => s.Estado == EstadoSolicitud.Rechazada);

        var resumenPorEstado = await query
            .GroupBy(s => s.Estado)
            .Select(g => new ResumenEstadoDto
            {
                Estado = g.Key.ToString(),
                Cantidad = g.Count()
            })
            .ToListAsync();

        // 1. Agrupamos y obtenemos los datos desde SQL Server
        var datosEvolucionRaw = await query
            .GroupBy(s => s.FechaCreacion.Date)
            .Select(g => new
            {
                Fecha = g.Key,
                Cantidad = g.Count()
            })
            .OrderBy(e => e.Fecha)
            .ToListAsync();

        // 2. Formateamos el string en memoria para evitar errores de traducción LINQ
        var evolucion = datosEvolucionRaw
            .Select(e => new EvolucionSemanalDto
            {
                Fecha = e.Fecha.ToString("dd/MM"),
                Cantidad = e.Cantidad
            })
            .ToList();

        return new DashboardResumenDto
        {
            TotalRegistros = total,
            RegistrosCompletados = completados,
            RegistrosPendientes = pendientes,
            RegistrosRechazados = rechazados,
            ResumenPorEstado = resumenPorEstado,
            Evolucion = evolucion
        };
    }
}