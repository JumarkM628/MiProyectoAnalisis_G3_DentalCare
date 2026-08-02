using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Expediente
{
    public interface IReporteProcedimientosLN
    {
        List<ReporteProcedimientosDto> ObtenerProcedimientosPorExpediente(int idExpediente);

        List<ReporteProcedimientosDto> ObtenerProcedimientosPorExpedienteFiltrado(
            int idExpediente, DateTime? fechaInicio, DateTime? fechaFin);

        List<ExpedienteItemDto> ObtenerExpedientes();
    }
}
