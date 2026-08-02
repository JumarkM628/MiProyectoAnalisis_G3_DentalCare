using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Producto
{
    public class ReporteProductosVencerLN : IReporteProductosVencerLN
    {
        private readonly IReporteProductosVencerAD _reporteProductosVencerAD;

        public ReporteProductosVencerLN(IReporteProductosVencerAD reporteProductosVencerAD)
        {
            _reporteProductosVencerAD = reporteProductosVencerAD;
        }

        public List<ReporteProductosVencerDto> ObtenerProductosPorVencer(
            DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
                throw new ArgumentException(
                    "La fecha inicial no puede ser mayor que la fecha final.");

            if (!fechaInicio.HasValue && !fechaFin.HasValue)
            {
                fechaInicio = DateTime.Today;
                fechaFin = DateTime.Today.AddDays(30);
            }

            return _reporteProductosVencerAD.ObtenerProductosPorVencer(fechaInicio, fechaFin);
        }
    }
}