using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Finanzas;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Finanzas;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Finanzas
{
    public class ReporteGastosLN : IReporteGastosLN
    {
        private readonly IReporteGastosAD _reporteGastosAD;

        public ReporteGastosLN(IReporteGastosAD reporteGastosAD)
        {
            _reporteGastosAD = reporteGastosAD;
        }

        public List<ReporteGastosDto> ObtenerGastos(DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
                throw new ArgumentException(
                    "La fecha inicial no puede ser mayor que la fecha final.");

            return _reporteGastosAD.ObtenerGastos(fechaInicio, fechaFin);
        }
    }
}
