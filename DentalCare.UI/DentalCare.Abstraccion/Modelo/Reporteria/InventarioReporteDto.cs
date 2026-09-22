using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class InventarioReporteDto
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public string Estado { get; set; }
    }
}
