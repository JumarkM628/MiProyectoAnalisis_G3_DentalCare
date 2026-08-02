using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class ReporteCitasCanceladasDto
    {
        public int IdCita { get; set; }
        public int IdUsuario { get; set; }
        public string NombrePaciente { get; set; }
        public DateTime? FechaCita { get; set; }
        public TimeSpan? HoraCita { get; set; }
        public string MotivoCita { get; set; }
        public string EstadoCita { get; set; }
        public string MotivoCancelacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
    }
}
