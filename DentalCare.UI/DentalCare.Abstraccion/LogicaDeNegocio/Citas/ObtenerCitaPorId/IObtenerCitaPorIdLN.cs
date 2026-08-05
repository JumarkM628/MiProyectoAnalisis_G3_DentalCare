using DentalCare.Abstraccion.Modelo.Citas;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerCitaPorId
{
    public interface IObtenerCitaPorIdLN
    {
        CitaDto Obtener(int idCita);
    }
}