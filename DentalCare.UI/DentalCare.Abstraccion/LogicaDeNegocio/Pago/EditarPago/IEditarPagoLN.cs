using DentalCare.Abstraccion.Modelo.Pagos;
using System.Collections.Generic;
namespace DentalCare.Abstraccion.LogicaDeNegocio.Pago.EditarPago
{
    public interface IEditarPagoLN
    {
        string Actualizar(int idContabilidad, decimal monto, int idMetodoPago);
    }
}