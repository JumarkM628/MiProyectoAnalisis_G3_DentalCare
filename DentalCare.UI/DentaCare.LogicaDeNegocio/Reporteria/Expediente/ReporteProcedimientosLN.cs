using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Expediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Expediente;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Expediente
{
    public class ReporteProcedimientosLN : IReporteProcedimientosLN
    {
        private readonly IReporteProcedimientosAD _reporteProcedimientosAD;

        public ReporteProcedimientosLN(IReporteProcedimientosAD reporteProcedimientosAD)
        {
            _reporteProcedimientosAD = reporteProcedimientosAD;
        }
        public List<ReporteProcedimientosDto> ObtenerProcedimientosPorExpediente(int idExpediente)
        {
            if (idExpediente <= 0)
                throw new ArgumentException("Debe seleccionar un expediente válido.");

            return _reporteProcedimientosAD.ObtenerProcedimientosPorExpediente(idExpediente);
        }
        public List<ReporteProcedimientosDto> ObtenerProcedimientosPorExpedienteFiltrado(
            int idExpediente, DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (idExpediente <= 0)
                throw new ArgumentException("Debe seleccionar un expediente válido.");

            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
                throw new ArgumentException(
                    "La fecha inicial no puede ser mayor que la fecha final.");

            return _reporteProcedimientosAD.ObtenerProcedimientosPorExpedienteFiltrado(
                idExpediente, fechaInicio, fechaFin);
        }

        public List<ExpedienteItemDto> ObtenerExpedientes()
        {
            return _reporteProcedimientosAD.ObtenerExpedientes();
        }
    }
}

