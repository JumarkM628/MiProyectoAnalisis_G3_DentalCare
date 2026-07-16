using DentalCare.Abstraccion.Modelo.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Pago.ActualizarEstadoPago
{
    public interface IActualizarEstadoPagoLN
    {
        string RegistrarPendiente(int idContabilidad);
        string ActualizarAPagado(int idContabilidad);
        PagoDto ObtenerPorId(int idContabilidad);
    }
}
