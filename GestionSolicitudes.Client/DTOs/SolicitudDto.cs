using GestionSolicitudes.Client.DTOs;

namespace GestionSolicitudes.Client.DTOs;

public class SolicitudDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int  Estado { get; set; }
    public string Solicitante { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}

public class CrearSolicitudDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Solicitante { get; set; } = string.Empty;

}

public class RespuestaPaginaDto<T>
{
   public int TotalRegistros { get; set; }
    public int NumeroPagina { get; set; }
    public int TamanoPagina { get; set; }

    public List<T> Datos { get; set; } = new();
}
