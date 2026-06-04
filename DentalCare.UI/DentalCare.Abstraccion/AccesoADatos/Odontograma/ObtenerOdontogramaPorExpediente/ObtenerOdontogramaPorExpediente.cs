using DentalCare.Abstraccion.Modelo.Odontograma;

namespace DentalCare.Abstraccion.AccesoADatos.Odontograma.ObtenerOdontogramaPorExpediente
{
    public interface IObtenerOdontogramaPorExpedienteAD
    {
        OdontogramaDto Obtener(int idExpediente);
    }
}