using System.Collections.Generic;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Producto
{
    public class ReporteLotesLN : IReporteLotesLN
    {
        private readonly IReporteLotesAD _reporteLotesAD;

        public ReporteLotesLN(IReporteLotesAD reporteLotesAD)
        {
            _reporteLotesAD = reporteLotesAD;
        }
        public List<ProductoLoteFrecuenciaDto> ObtenerLotesMasUtilizados()
            => _reporteLotesAD.ObtenerLotesMasUtilizados();
        public List<ProductoLoteFrecuenciaDto> ObtenerLotesMenosUtilizados()
            => _reporteLotesAD.ObtenerLotesMenosUtilizados();
        public List<ProductoLoteFrecuenciaDto> ObtenerLotesMasComprados()
            => _reporteLotesAD.ObtenerLotesMasComprados();
        public List<ProductoLoteFrecuenciaDto> ObtenerLotesMenosComprados()
            => _reporteLotesAD.ObtenerLotesMenosComprados();
        public List<HistorialLoteTratamientoDto> ObtenerHistorialLotePorTratamiento()
            => _reporteLotesAD.ObtenerHistorialLotePorTratamiento();
    }
}
