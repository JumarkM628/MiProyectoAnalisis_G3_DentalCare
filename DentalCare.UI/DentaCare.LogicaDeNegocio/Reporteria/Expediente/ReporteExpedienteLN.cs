using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Expediente;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Reporteria.Expediente;

namespace DentaCare.LogicaDeNegocio.Reporteria.Expediente
{
    public class ReporteExpedienteLN
    {
        private readonly ReporteExpedienteAD _ad;

        public ReporteExpedienteLN()
        {
            _ad = new ReporteExpedienteAD(new Contexto());
        }

        public ReporteExpedienteLN(ReporteExpedienteAD ad)
        {
            _ad = ad ?? new ReporteExpedienteAD(new Contexto());
        }
        public List<ProcedimientoDto> ObtenerProcedimientosPorExpediente(int idExpediente, DateTime? desde = null, DateTime? hasta = null, int? idTratamiento = null)
        {
            if (desde.HasValue && hasta.HasValue && desde.Value.Date > hasta.Value.Date)
                throw new ArgumentException("El rango de fechas ingresado no es válido.");
            return _ad.ObtenerProcedimientosPorExpediente(idExpediente, desde, hasta, idTratamiento);
        }
    }
}
