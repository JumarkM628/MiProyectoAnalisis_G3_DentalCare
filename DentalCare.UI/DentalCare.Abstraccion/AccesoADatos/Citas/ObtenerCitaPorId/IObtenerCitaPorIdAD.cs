using DentalCare.Abstraccion.Modelo.Citas;

namespace DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerCitaPorId
{
    public interface IObtenerCitaPorIdAD
    {
        CitaDto Obtener(int idCita);
    }
}