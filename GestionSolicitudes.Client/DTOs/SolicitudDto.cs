namespace GestionSolicitudes.Client.DTOs;

public class SolicitudDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}

public class CrearSolicitudDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Solicitante { get; set; } = string.Empty;

}

public class ActualizarSoliciudDto 
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Solicitante { get; set; } = string.Empty;


}

public class ActualizarEstadoDto
{
    public string NuevoEstado { get; set; } = string.Empty;
}