using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentalCare.Abstraccion.Modelo.Expediente
{
    public class ProcedimientosExpedienteModelDto
    {
        public int IdExpediente { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public int? IdTratamiento { get; set; }
        public ExpedienteDetalleDto Detalle { get; set; }   // ← nueva
        public List<ProcedimientoDto> Procedimientos { get; set; } = new List<ProcedimientoDto>();
    }
}