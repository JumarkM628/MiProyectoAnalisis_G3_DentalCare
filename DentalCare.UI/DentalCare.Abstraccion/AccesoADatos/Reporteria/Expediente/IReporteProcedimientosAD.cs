using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.AccesoADatos.Reporteria.Expediente
{
    public interface IReporteProcedimientosAD
    {
        List<ReporteProcedimientosDto> ObtenerProcedimientosPorExpediente(int idExpediente);
        List<ReporteProcedimientosDto> ObtenerProcedimientosPorExpedienteFiltrado(
            int idExpediente, DateTime? fechaInicio, DateTime? fechaFin);
        List<ExpedienteItemDto> ObtenerExpedientes();
    }
}
