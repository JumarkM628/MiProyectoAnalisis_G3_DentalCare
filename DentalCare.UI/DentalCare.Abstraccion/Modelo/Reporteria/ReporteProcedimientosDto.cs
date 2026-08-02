using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class ReporteProcedimientosDto
    {
        public int IdProcedimiento { get; set; }
        public int IdExpediente { get; set; }
        public string NombrePaciente { get; set; }
        public string DescripcionProcedimiento { get; set; }
        public DateTime? FechaProcedimiento { get; set; }
        public string Observaciones { get; set; }
        public string PlanTratamiento { get; set; }
        public string EstadoProcedimiento { get; set; }
    }
}
