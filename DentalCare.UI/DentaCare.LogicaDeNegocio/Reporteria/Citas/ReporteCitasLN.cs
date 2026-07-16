using System;
using System.Collections.Generic;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Citas;
using DentalCare.Abstraccion.AccesoADatos.Citas.Reporteria;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Citas
{
    public class ReporteCitasLN : IReporteCitasLN
    {
        private readonly IReporteCitasAD _reporteCitasAD;

        public ReporteCitasLN(IReporteCitasAD reporteCitasAD)
        {
            _reporteCitasAD = reporteCitasAD;
        }

        public List<CitaReporteDto> ObtenerPorPeriodo(DateTime desde, DateTime hasta)
        {
            return _reporteCitasAD.ObtenerPorPeriodo(desde, hasta);
        }
    }
}
