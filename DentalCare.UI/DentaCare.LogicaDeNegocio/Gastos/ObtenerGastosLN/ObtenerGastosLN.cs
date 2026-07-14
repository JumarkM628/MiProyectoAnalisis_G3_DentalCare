using DentalCare.Abstraccion.AccesoADatos.Gastos.ObtenerGastos;
using DentalCare.Abstraccion.LogicaDeNegocio.Gastos.ObtenerGastos;
using DentalCare.Abstraccion.Modelo.Gasto;
using DentalCare.AccesoADatos.Gastos.ObtenerGastos;
using System.Collections.Generic;

namespace DentalCare.LogicaDeNegocio.Gastos.ObtenerGastos
{
    public class ObtenerGastosLN : IObtenerGastosLN
    {
        private readonly IObtenerGastosAD _obtenerAD;

        public ObtenerGastosLN()
        {
            _obtenerAD = new ObtenerGastosAD();
        }

        public List<GastoDto> Obtener()
        {
            return _obtenerAD.Obtener();
        }
    }
}