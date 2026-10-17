using DentalCare.Abstraccion.AccesoADatos.Pago.EditarPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.EditarPago;
using DentalCare.AccesoADatos.Pago.EditarPago;

namespace DentaCare.LogicaDeNegocio.Pago.EditarPago
{
    public class EditarPagoLN : IEditarPagoLN
    {
        private readonly IEditarPagoAD _ad;

        public EditarPagoLN()
        {
            _ad = new EditarPagoAD();
        }

        // Devuelve null si todo salió bien, o el mensaje de error
        public string Actualizar(int idContabilidad, decimal monto, int idMetodoPago)
        {
            if (!_ad.Existe(idContabilidad))
                return "El pago indicado no existe.";

            if (_ad.EstaPagado(idContabilidad))
                return "El pago ya está cerrado (pagado) y no puede modificarse.";

            if (monto <= 0)
                return "El monto debe ser mayor a cero.";

            _ad.Actualizar(idContabilidad, monto, idMetodoPago);
            return null;
        }
    }
}