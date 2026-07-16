using DentalCare.Abstraccion.AccesoADatos.Pago.ObtenerPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.ObtenerPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using DentalCare.AccesoADatos.Pago.ObtenerPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Pago.ObtenerPago
{
    public class ObtenerPagosLN : IObtenerPagosLN
    {
        private readonly IObtenerPagosAD _obtenerAD;

        public ObtenerPagosLN()
        {
            _obtenerAD = new ObtenerPagosAD();
        }

        public List<PagoDto> Obtener()
        {
            return _obtenerAD.Obtener();
        }
    }
}
