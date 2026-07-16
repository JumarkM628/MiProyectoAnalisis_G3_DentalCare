using DentalCare.Abstraccion.Modelo.Gasto;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Gastos
{
    public interface IEditarGastoLN
    {
        GastoDto ObtenerGastoPorId(int id);
        string Editar(GastoDto dto, string nombreUsuario);
    }
}
