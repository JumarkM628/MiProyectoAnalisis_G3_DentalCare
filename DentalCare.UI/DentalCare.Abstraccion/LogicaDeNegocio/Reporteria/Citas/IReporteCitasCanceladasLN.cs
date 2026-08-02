using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Citas
{
    public interface IReporteCitasCanceladasLN
    {
        List<ReporteCitasCanceladasDto> ObtenerCitasCanceladas(
            DateTime? fechaInicio, DateTime? fechaFin);
    }
}