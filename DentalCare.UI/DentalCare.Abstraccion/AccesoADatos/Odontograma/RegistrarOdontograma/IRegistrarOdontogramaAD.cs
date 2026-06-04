using DentalCare.Abstraccion.Modelo.Odontograma;

namespace DentalCare.Abstraccion.AccesoADatos.Odontograma.RegistrarOdontograma
{
    public interface IRegistrarOdontogramaAD
    {
        void Registrar(OdontogramaDto dto);
    }
}