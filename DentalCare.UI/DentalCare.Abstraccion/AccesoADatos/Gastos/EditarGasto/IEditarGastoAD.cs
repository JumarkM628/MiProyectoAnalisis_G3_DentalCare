using DentalCare.Abstraccion.Modelo.Gasto;

namespace DentalCare.Abstraccion.AccesoADatos.Gastos
{
    public interface IEditarGastoAD
    {
        GastoDto ObtenerGastoPorId(int id);
        void Editar(GastoDto dto, string nombreUsuario);
    }
}
