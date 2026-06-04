using DentalCare.Abstraccion.AccesoADatos.Odontograma.ObtenerOdontogramaPorExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Odontograma.ObtenerOdontogramaPorExpediente;
using DentalCare.Abstraccion.Modelo.Odontograma;
using DentalCare.AccesoADatos.Odontograma.ObtenerOdontogramaPorExpediente;

namespace DentalCare.LogicaDeNegocio.Odontograma.ObtenerOdontogramaPorExpediente
{
    public class ObtenerOdontogramaPorExpedienteLN : IObtenerOdontogramaPorExpedienteLN
    {
        private readonly IObtenerOdontogramaPorExpedienteAD _obtenerAD;

        public ObtenerOdontogramaPorExpedienteLN()
        {
            _obtenerAD = new ObtenerOdontogramaPorExpedienteAD();
        }

        public OdontogramaDto Obtener(int idExpediente)
        {
            return _obtenerAD.Obtener(idExpediente);
        }
    }
}