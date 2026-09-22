using System;

namespace DentalCare.Abstraccion.Modelo.Expediente
{
    public class ProcedimientoDto
    {
        public int IdProcedimiento { get; set; }
        public int? IdCita { get; set; }
        public int? IdTratamiento { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Observaciones { get; set; }
        public string NombreDoctor { get; set; }
        public string NombreTratamiento { get; set; }
    }
}
