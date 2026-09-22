using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Cita;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Citas;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Citas
{
    public class ReporteCitasCanceladasLN : IReporteCitasCanceladasLN
    {
        private readonly IReporteCitasCanceladasAD _reporteCitasCanceladasAD;

        public ReporteCitasCanceladasLN(IReporteCitasCanceladasAD reporteCitasCanceladasAD)
        {
            _reporteCitasCanceladasAD = reporteCitasCanceladasAD;
        }

        public List<ReporteCitasCanceladasDto> ObtenerCitasCanceladas(
            DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
                throw new ArgumentException(
                    "La fecha inicial no puede ser mayor que la fecha final.");

            return _reporteCitasCanceladasAD.ObtenerCitasCanceladas(fechaInicio, fechaFin);
        }
    }
}

