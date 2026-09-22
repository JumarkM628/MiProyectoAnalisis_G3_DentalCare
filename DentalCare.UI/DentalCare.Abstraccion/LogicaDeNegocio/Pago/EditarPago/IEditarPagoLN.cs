using DentalCare.Abstraccion.Modelo.Pagos;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Pago.EditarPago
{
    public interface IEditarPagoLN
    {
        PagoDto ObtenerPorId(int idContabilidad);
        string Editar(PagoDto dto, string nombreUsuario);
    }
}
