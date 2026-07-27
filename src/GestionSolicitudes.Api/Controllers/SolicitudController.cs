using GestionSolicitudes.Application.DTOs;
using GestionSolicitudes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionSolicitudes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudController(ISolicitudService solicitudService) : ControllerBase
{
    private readonly ISolicitudService _solicitudService = solicitudService;
    /// <summary>
    /// Sirve para obtener las solicitudes
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var solicitudes = await _solicitudService.ObtenerTodasAsync();
        return Ok(solicitudes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var solicitud = await _solicitudService.ObtenerPorIdAsync(id);
        if (solicitud == null) return NotFound("Solicitud no encontrada.");
        return Ok(solicitud);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearSolicitudDto dto)
    {
        var creada = await _solicitudService.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
    }

    // PUT: /api/Solicitud/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarSolicitudDto dto)
    {
        var resultado = await _solicitudService.ActualizarAsync(id, dto);
        if (!resultado) return NotFound("La solicitud no existe o está inactiva.");
        return NoContent();
    }

    // PATCH: /api/Solicitud/{id}/estado
    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] ActualizarEstadoDto dto)
    {
        var resultado = await _solicitudService.CambiarEstadoAsync(id, dto);
        if (!resultado) return NotFound("La solicitud no existe o está inactiva.");
        return NoContent();
    }

    // DELETE: /api/Solicitud/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var resultado = await _solicitudService.DesactivarAsync(id);
        if (!resultado) return NotFound("La solicitud no existe o está inactiva.");
        return NoContent();
    }
}