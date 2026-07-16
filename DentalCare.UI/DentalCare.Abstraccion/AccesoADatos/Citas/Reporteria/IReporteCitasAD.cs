using System;
using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.AccesoADatos.Citas.Reporteria
{
    public interface IReporteCitasAD
    {
        List<CitaReporteDto> ObtenerPorPeriodo(DateTime desde, DateTime hasta);
    }
}
