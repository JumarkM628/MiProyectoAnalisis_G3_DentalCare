using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Expediente
{
    public class ProcedimientosExpedienteModelDto
    {
        public int IdExpediente { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public int? IdTratamiento { get; set; }  
        public List<ProcedimientoDto> Procedimientos { get; set; } = new List<ProcedimientoDto>();
    }
}
