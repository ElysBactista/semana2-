using System.Threading.Tasks;
using GestionSolicitudes.Application.DTOs;
using GestionSolicitudes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionSolicitudes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudController(ISolicitudService solicitudService) : ControllerBase
{
    /// <summary>
    /// Sirve para obtener las solicitudes paginadas y filtradas
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerTodas(
        [FromQuery] string? busqueda,
        [FromQuery] int numeroPagina = 1,
        [FromQuery] int tamanoPagina = 10)
    {
        var (Total, Solicitudes) = await solicitudService.ObtenerTodasAsync(busqueda, numeroPagina, tamanoPagina);

        return Ok(new
        {
            TotalRegistros = Total,
            NumeroPagina = numeroPagina,
            TamanoPagina = tamanoPagina,
            Datos = Solicitudes
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var solicitud = await solicitudService.ObtenerPorIdAsync(id);
        if (solicitud == null) return NotFound("Solicitud no encontrada.");
        return Ok(solicitud);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearSolicitudDto dto)
    {
        var creada = await solicitudService.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
    }

    // PUT: /api/Solicitud/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarSolicitudDto dto)
    {
        var resultado = await solicitudService.ActualizarAsync(id, dto);
        if (!resultado) return NotFound("La solicitud no existe o está inactiva.");
        return Ok("Solicitud actualizada correctamente.");
    }

    // PATCH: /api/Solicitud/{id}/estado
    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] ActualizarEstadoDto dto)
    {
        var resultado = await solicitudService.CambiarEstadoAsync(id, dto);
        if (!resultado) return NotFound("La solicitud no existe o está inactiva.");
        return NoContent();
    }

    // DELETE: /api/Solicitud/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var resultado = await solicitudService.DesactivarAsync(id);
        if (!resultado) return NotFound("La solicitud no existe o está inactiva.");
        return NoContent();
    }
}