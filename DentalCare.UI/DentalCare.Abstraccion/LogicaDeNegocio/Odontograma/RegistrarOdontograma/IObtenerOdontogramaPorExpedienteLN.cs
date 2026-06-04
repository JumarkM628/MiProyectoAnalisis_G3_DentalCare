using DentalCare.Abstraccion.Modelo.Odontograma;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Odontograma.ObtenerOdontogramaPorExpediente
{
    public interface IObtenerOdontogramaPorExpedienteLN
    {
        OdontogramaDto Obtener(int idExpediente);
    }
}