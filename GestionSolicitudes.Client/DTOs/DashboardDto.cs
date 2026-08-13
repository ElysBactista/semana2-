using System.Collections.Generic;
using GestionSolicitudes.Client.DTOs;

namespace GestionSolicitudes.Client.DTOs
{

    public class DashboardResumenDto
    {
        public int TotalRegistros { get; set; }
        public int RegistrosCompletados { get; set; }
        public int RegistrosPendientes { get; set; }
        public int RegistrosRechazados { get; set; }
        public List<ResumenEstadoDto> ResumenPorEstado { get; set; } = new List<ResumenEstadoDto>();
        public List<EvolucionSemanalDto> Evolucion { get; set; } = new List<EvolucionSemanalDto>();
    }

    public class ResumenEstadoDto 
    {
        public string Estado { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class EvolucionSemanalDto
    {
        public string Fecha { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

}

