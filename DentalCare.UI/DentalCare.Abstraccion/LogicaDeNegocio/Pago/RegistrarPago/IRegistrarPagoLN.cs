using DentalCare.Abstraccion.Modelo.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Pago.RegistrarPago
{
    public interface IRegistrarPagoLN
    {
        string Registrar(PagoDto dto);
    }
}
