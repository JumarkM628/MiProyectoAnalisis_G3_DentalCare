using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto
{
    public interface IReporteProductosLN
    {
        List<ProductoFrecuenciaDto> ObtenerMasUtilizados();
        List<ProductoFrecuenciaDto> ObtenerMenosUtilizados();
        List<ProductoFrecuenciaDto> ObtenerMasComprados();
        List<ProductoFrecuenciaDto> ObtenerMenosComprados();
        List<HistorialTratamientoDto> ObtenerHistorialPorTratamiento();
    }
}