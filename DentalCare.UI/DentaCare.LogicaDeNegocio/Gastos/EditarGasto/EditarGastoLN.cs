using DentalCare.Abstraccion.AccesoADatos.Gastos;
using DentalCare.Abstraccion.LogicaDeNegocio.Gastos;
using DentalCare.Abstraccion.Modelo.Gasto;
using DentalCare.AccesoADatos.Gastos.EditarGasto;

namespace DentalCare.LogicaDeNegocio.Gastos.EditarGasto
{
    public class EditarGastoLN : IEditarGastoLN
    {
        private readonly IEditarGastoAD _editarAD;

        public EditarGastoLN()
        {
            _editarAD = new EditarGastoAD();
        }

        public GastoDto ObtenerGastoPorId(int id)
        {
            return _editarAD.ObtenerGastoPorId(id);
        }

        public string Editar(GastoDto dto, string nombreUsuario)
        {
            var gastoActual = _editarAD.ObtenerGastoPorId(dto.IdGasto);
            if (gastoActual == null)
                return "El gasto no fue encontrado.";

            _editarAD.Editar(dto, nombreUsuario);
            return null;
        }
    }
}
