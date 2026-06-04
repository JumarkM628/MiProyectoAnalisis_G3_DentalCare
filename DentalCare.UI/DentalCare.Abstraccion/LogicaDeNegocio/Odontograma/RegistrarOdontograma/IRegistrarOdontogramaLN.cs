using DentalCare.Abstraccion.Modelo.Odontograma;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Odontograma.RegistrarOdontograma
{
    public interface IRegistrarOdontogramaLN
    {
        string Registrar(OdontogramaDto dto);
    }
}