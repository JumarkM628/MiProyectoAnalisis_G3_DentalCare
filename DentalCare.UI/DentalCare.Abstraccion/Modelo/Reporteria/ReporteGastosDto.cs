using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class ReporteGastosDto
    {
        public int IdGasto { get; set; }
        public string Descripcion { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
    }
}
