using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionSolicitudes.Application.Interfaces;

namespace GestionSolicitudes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService) => _dashboardService = dashboardService;

    [HttpGet]
    public async Task<IActionResult> GetDashboardResumen([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin)
    {
        try
        {
            var resumen = await _dashboardService.ObtenerResumenAsync(fechaInicio, fechaFin);
            return Ok(resumen);
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor al calcular los indicadores del dashboard.");
        }
    }
}