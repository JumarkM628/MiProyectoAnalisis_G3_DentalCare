using System;

namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class ProductoInventarioDto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CategoriaProducto { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Proveedor { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
