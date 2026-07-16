using DentalCare.Abstraccion.Modelo.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Pago.ObtenerPago
{
    public interface IObtenerPagosAD
    {
        List<PagoDto> Obtener();
    }
}
