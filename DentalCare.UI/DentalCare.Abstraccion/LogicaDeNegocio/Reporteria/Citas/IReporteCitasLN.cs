using System;
using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Citas
{
    public interface IReporteCitasLN
    {
        List<CitaReporteDto> ObtenerPorPeriodo(DateTime desde, DateTime hasta);
    }
}
