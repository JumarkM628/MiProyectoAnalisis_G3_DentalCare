using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto
{
    public interface IReporteLotesAD
    {
        List<ProductoLoteFrecuenciaDto> ObtenerLotesMasUtilizados();
        List<ProductoLoteFrecuenciaDto> ObtenerLotesMenosUtilizados();
        List<ProductoLoteFrecuenciaDto> ObtenerLotesMasComprados();
        List<ProductoLoteFrecuenciaDto> ObtenerLotesMenosComprados();
        List<HistorialLoteTratamientoDto> ObtenerHistorialLotePorTratamiento();
    }
}
