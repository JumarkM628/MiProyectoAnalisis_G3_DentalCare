using DentalCare.Abstraccion.AccesoADatos.Pago.EditarPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.EditarPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using DentalCare.AccesoADatos.Pago.EditarPago;

namespace DentalCare.LogicaDeNegocio.Pagos.EditarPago
{
    public class EditarPagoLN : IEditarPagoLN
    {
        private readonly IEditarPagoAD _editarAD;

        public EditarPagoLN()
        {
            _editarAD = new EditarPagoAD();
        }

        public PagoDto ObtenerPorId(int idContabilidad)
        {
            return _editarAD.ObtenerPorId(idContabilidad);
        }

        // MDC-009: edita monto, método de pago, fecha y estado de un pago registrado
        public string Editar(PagoDto dto, string nombreUsuario)
        {
            var pagoActual = _editarAD.ObtenerPorId(dto.IdContabilidad);
            if (pagoActual == null)
                return "No se encontró el pago indicado.";

            // Escenario 5: un pago ya pagado queda cerrado y no puede editarse
            if (_editarAD.EstaPagado(dto.IdContabilidad))
                return "El pago ya está cerrado y no puede editarse.";

            if (!dto.IdMetodoPago.HasValue || dto.IdMetodoPago.Value <= 0 || dto.IdEstado <= 0)
                return "Debe completar todos los campos obligatorios.";

            if (!dto.FechaPago.HasValue)
                return "La fecha es obligatoria.";

            // Escenario 3: el monto debe ser numérico y mayor a cero
            if (dto.Monto <= 0)
                return "El monto no es válido: debe ser un número mayor a cero.";

            _editarAD.Actualizar(dto, nombreUsuario);

            return null;
        }
    }
}
