using DentalCare.Abstraccion.AccesoADatos.Expediente.Alerta.ObtenerAlertaPorExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.Alertas.ObtenerAlertasPorExpediente;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Alertas.ObtenerAlertaPorExpediente;

namespace DentalCare.LogicaDeNegocio.Alertas.ObtenerAlertaPorExpediente
{
    public class ObtenerAlertaPorExpedienteLN : IObtenerAlertaPorExpedienteLN
    {
        private readonly IObtenerAlertaPorExpedienteAD _obtenerAD;

        public ObtenerAlertaPorExpedienteLN()
        {
            _obtenerAD = new ObtenerAlertaPorExpedienteAD();
        }

        public ExpedienteDetalleDto Obtener(int idExpediente)
        {
            return _obtenerAD.Obtener(idExpediente);
        }
    }
}
