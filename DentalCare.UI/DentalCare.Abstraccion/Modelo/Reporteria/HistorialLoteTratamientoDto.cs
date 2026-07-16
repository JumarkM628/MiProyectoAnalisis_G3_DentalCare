using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class HistorialLoteTratamientoDto
    {
        public string NombreTratamiento { get; set; }
        public string NombreProducto { get; set; }
        public string Lote { get; set; }
        public int CantidadTotal { get; set; }
    }
}
