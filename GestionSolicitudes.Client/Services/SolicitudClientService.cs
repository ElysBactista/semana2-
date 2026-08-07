using System.Net.Http.Json;
using GestionSolicitudes.Client.DTOs;

namespace GestionSolicitudes.Client.Services;

public class SolicitudClientService(HttpClient http)
{
    private readonly HttpClient _http = http;

    // Obtener todas las solicitudes con filtro y paginación
    public async Task<(int Total, List<SolicitudDto> Solicitudes)> ObtenerTodasAsync(string? busqueda = null, int pagina = 1, int tamanoPagina = 10)
    {
        var url = $"api/solicitudes?busqueda={busqueda}&numeroPagina={pagina}&tamanoPagina={tamanoPagina}";
        var response = await _http.GetFromJsonAsync<ResultadoPaginadoDto>(url);

        return response != null
            ? (response.Total, response.Solicitudes)
            : (0, []);
    }

    // Obtener solicitud por ID
    public async Task<SolicitudDto?> ObtenerPorIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<SolicitudDto>($"api/solicitudes/{id}");
    }

    // Crear solicitud
    public async Task<bool> CrearAsync(CrearSolicitudDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/solicitudes", dto);
        return response.IsSuccessStatusCode;
    }

    // Actualizar solicitud
    public async Task<bool> ActualizarAsync(int id, ActualizarSolicitudDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/solicitudes/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    // Cambiar estado
    public async Task<bool> CambiarEstadoAsync(int id, ActualizarEstadoDto dto)
    {
        var response = await _http.PatchAsJsonAsync($"api/solicitudes/{id}/estado", dto);
        return response.IsSuccessStatusCode;
    }

    // Desactivar / Eliminar
    public async Task<bool> DesactivarAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/solicitudes/{id}");
        return response.IsSuccessStatusCode;
    }
}

public class ResultadoPaginadoDto
{
    public int Total { get; set; }
    public List<SolicitudDto> Solicitudes { get; set; } = [];




}