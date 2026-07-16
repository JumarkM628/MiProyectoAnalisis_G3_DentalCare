using System.Collections.Generic;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Producto
{
    public class ReporteProductosLN : IReporteProductosLN
    {
        private readonly IReporteProductosAD _reporteProductosAD;
        public ReporteProductosLN(IReporteProductosAD reporteProductosAD)
        {
            _reporteProductosAD = reporteProductosAD;
        }
        public List<ProductoFrecuenciaDto> ObtenerMasUtilizados()
        {
            return _reporteProductosAD.ObtenerMasUtilizados();
        }
        public List<ProductoFrecuenciaDto> ObtenerMenosUtilizados()
        {
            return _reporteProductosAD.ObtenerMenosUtilizados();
        }
        public List<ProductoFrecuenciaDto> ObtenerMasComprados()
        {
            return _reporteProductosAD.ObtenerMasComprados();
        }
        public List<ProductoFrecuenciaDto> ObtenerMenosComprados()
        {
            return _reporteProductosAD.ObtenerMenosComprados();
        }
        public List<HistorialTratamientoDto> ObtenerHistorialPorTratamiento()
        {
            return _reporteProductosAD.ObtenerHistorialPorTratamiento();
        }
    }
}
