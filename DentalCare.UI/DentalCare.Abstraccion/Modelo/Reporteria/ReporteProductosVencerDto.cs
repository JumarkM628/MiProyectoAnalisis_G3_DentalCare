using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class ReporteProductosVencerDto
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CategoriaProducto { get; set; }
        public int? StockActual { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Proveedor { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}