using System;

namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class CitaReporteDto
    {
        public int IdCita { get; set; }
        public DateTime? Fecha { get; set; }
        public string HoraString { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreDoctor { get; set; }
        public string NombreMotivo { get; set; }
        public string NombreEstado { get; set; }
    }
}
