using DentalCare.Abstraccion.AccesoADatos.Gastos;
using DentalCare.Abstraccion.LogicaDeNegocio.Gastos;
using DentalCare.Abstraccion.Modelo.Gasto;
using DentalCare.AccesoADatos.Gastos.RegistrarGasto;

namespace DentalCare.LogicaDeNegocio.Gastos.RegistrarGasto
{
    public class RegistrarGastoLN : IRegistrarGastoLN
    {
        private readonly IRegistrarGastoAD _registrarAD;

        public RegistrarGastoLN()
        {
            _registrarAD = new RegistrarGastoAD();
        }

        public string Registrar(GastoDto dto)
        {
            _registrarAD.Registrar(dto);
            return null;
        }
    }
}