using System.Collections.Generic;

namespace GestionSolicitudes.Application.DTOs;

public class DashboardResumenDto
{
    public int TotalRegistros { get; set; }
    public int RegistrosCompletados { get; set; }
    public int RegistrosPendientes { get; set; }
    public int RegistrosRechazados { get; set; }

    public List<EvolucionSemanalDto> Evolucion { get; set; } = [];
    public List<ResumenEstadoDto> ResumenPorEstado { get; set; } = [];
}