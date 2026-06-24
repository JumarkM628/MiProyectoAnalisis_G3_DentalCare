using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto
{
    public interface IReporteProductosAD
    {
        List<ProductoFrecuenciaDto> ObtenerMasUtilizados();
        List<ProductoFrecuenciaDto> ObtenerMenosUtilizados();
        List<ProductoFrecuenciaDto> ObtenerMasComprados();
        List<ProductoFrecuenciaDto> ObtenerMenosComprados();
        List<HistorialTratamientoDto> ObtenerHistorialPorTratamiento();
    }
}
