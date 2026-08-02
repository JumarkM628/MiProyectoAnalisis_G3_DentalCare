using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Reporteria;
using DentalCare.AccesoADatos.Reporteria.Inventario;

namespace DentaCare.LogicaDeNegocio.Reporteria.Inventario
{
    public class ReporteInventarioLN
    {
        private readonly ReporteInventarioAD _reporteAD;
        public ReporteInventarioLN(ReporteInventarioAD reporteAD)
        {
            _reporteAD = reporteAD;
        }
        public List<ProductoInventarioDto> ObtenerProductosStockBajo(int? categoriaId = null)
        {
            return _reporteAD.ObtenerProductosStockBajo(categoriaId);
        }
    }
}
