using System;
using System.Threading.Tasks;
using GestionSolicitudes.Application.DTOs;

namespace GestionSolicitudes.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardResumenDto> ObtenerResumenAsync(DateTime? fechaInicio, DateTime? fechaFin);
}