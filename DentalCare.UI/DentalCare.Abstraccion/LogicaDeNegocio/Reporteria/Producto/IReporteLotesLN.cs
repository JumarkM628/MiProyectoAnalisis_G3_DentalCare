using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto
{
    public interface IReporteLotesLN
    {
        List<ProductoLoteFrecuenciaDto> ObtenerLotesMasUtilizados();
        List<ProductoLoteFrecuenciaDto> ObtenerLotesMenosUtilizados();
        List<ProductoLoteFrecuenciaDto> ObtenerLotesMasComprados();
        List<ProductoLoteFrecuenciaDto> ObtenerLotesMenosComprados();
        List<HistorialLoteTratamientoDto> ObtenerHistorialLotePorTratamiento();
    }
}