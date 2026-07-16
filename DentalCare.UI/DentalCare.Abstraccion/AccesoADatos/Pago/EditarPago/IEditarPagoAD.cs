using DentalCare.Abstraccion.Modelo.Pagos;

namespace DentalCare.Abstraccion.AccesoADatos.Pago.EditarPago
{
    public interface IEditarPagoAD
    {
        PagoDto ObtenerPorId(int idContabilidad);
        bool ExistePago(int idContabilidad);
        bool EstaPagado(int idContabilidad);
        void Actualizar(PagoDto dto, string nombreUsuario);
    }
}
