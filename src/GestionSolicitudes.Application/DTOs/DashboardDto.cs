using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSolicitudes.Application.DTOs
{
   public class DashboardDto
    {
        public int TotalSolicitudes { get; set; }
        public int Pendientes { get; set; }
        public int EnProceso { get; set; }
        public int Aprobadas { get; set; }
        public int Rechazadas { get; set; }
        public int Canceladas { get; set; }
       
    }
}
