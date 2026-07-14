using DentalCare.Abstraccion.AccesoADatos.Gastos.ActualizarEstadoPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.ActualizarEstadoPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using DentalCare.AccesoADatos.Pago.ActualizarEstadoPago;

namespace DentalCare.LogicaDeNegocio.Pagos.ActualizarEstadoPago
{
    public class ActualizarEstadoPagoLN : IActualizarEstadoPagoLN
    {
        private readonly IActualizarEstadoPagoAD _actualizarAD;
        private const int ESTADO_ACTIVO = 1; // Pagado
        private const int ESTADO_PENDIENTE = 5; // Pendiente

        public ActualizarEstadoPagoLN()
        {
            _actualizarAD = new ActualizarEstadoPagoAD();
        }

        // MDC-004: marcar pago como pendiente
        public string RegistrarPendiente(int idContabilidad)
        {
            if (!_actualizarAD.ExistePago(idContabilidad))
                return "No se encontró el pago indicado.";

            _actualizarAD.Actualizar(idContabilidad, ESTADO_PENDIENTE);
            return null;
        }

        // MDC-005: actualizar pago pendiente a pagado
        public string ActualizarAPagado(int idContabilidad)
        {
            if (!_actualizarAD.ExistePago(idContabilidad))
                return "No se encontró el pago indicado.";

            // Escenario 2 MDC-005: ya está pagado
            if (_actualizarAD.EstaPagado(idContabilidad))
                return "Este pago ya está registrado como pagado.";

            _actualizarAD.Actualizar(idContabilidad, ESTADO_ACTIVO);
            return null;
        }

        public PagoDto ObtenerPorId(int idContabilidad)
        {
            return _actualizarAD.ObtenerPorId(idContabilidad);
        }
    }
}