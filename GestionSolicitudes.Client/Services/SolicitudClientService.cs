using GestionSolicitudes.Client.DTOs;

using System.Net.Http.Json;

namespace GestionSolicitudes.Client.Services;

public class SolicitudClientService(HttpClient http)
{
    private readonly HttpClient _http = http;

    /// <summary>
    /// Consulta el listado paginado y filtrado de solicitudes
    /// </summary>
    public async Task<(int Total, List<SolicitudDto> Solicitudes)> ObtenerTodasAsync(string? busqueda = null, int pagina = 1, int tamanoPagina = 10)
    {
        var url = $"api/solicitud?busqueda={busqueda}&numeroPagina={pagina}&tamanoPagina={tamanoPagina}";
        var response = await _http.GetFromJsonAsync<ResultadoPaginadoDto>(url);

        return response != null
            ? (response.TotalRegistros, response.Datos)
            : (0, []);
    }

    /// <summary>
    /// Consulta el detalle de una solicitud específica por su ID
    /// </summary>
    public async Task<SolicitudDto?> ObtenerPorIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<SolicitudDto>($"api/solicitud/{id}");
    }

    /// <summary>
    /// Envía la creación de un nuevo expediente a la API
    /// </summary>
    public async Task<bool> CrearAsync(CrearSolicitudDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/solicitud", dto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Modifica título, solicitante o detalles de la solicitud
    /// </summary>
    public async Task<bool> ActualizarAsync(int id, ActualizarSolicitudDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/solicitud/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Actualiza únicamente el estado de la solicitud
    /// </summary>
    public async Task<bool> CambiarEstadoAsync(int id, ActualizarEstadoDto dto)
    {
        var response = await _http.PatchAsJsonAsync($"api/solicitud/{id}/estado", dto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Da de baja / elimina un expediente
    /// </summary>
    public async Task<bool> DesactivarAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/solicitud/{id}");
        return response.IsSuccessStatusCode;
    }
}

public class ResultadoPaginadoDto
{
    public int TotalRegistros { get; set; }
    public int NumeroPagina { get; set; }
    public int TamanoPagina { get; set; }
    public List<SolicitudDto> Datos { get; set; } = [];
}

public class ActualizarEstadoDto
{
    public string NuevoEstado { get; set; } = string.Empty;
}

public class ActualizarSolicitudDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Solicitante { get; set; } = string.Empty;
}