using DentalCare.Abstraccion.Modelo.Pagos;

namespace DentalCare.Abstraccion.AccesoADatos.Pago.EditarPago
{
    public interface IEditarPagoAD
    {
        bool Existe(int idContabilidad);
        bool EstaPagado(int idContabilidad);
        void Actualizar(int idContabilidad, decimal monto, int idMetodoPago);
    }
}
