using DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerCitaPorId;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerCitaPorId;
using DentalCare.Abstraccion.Modelo.Citas;
using DentalCare.AccesoADatos.Citas.ObtenerCitaPorId;

namespace DentalCare.LogicaDeNegocio.Citas.ObtenerCitaPorId
{
    public class ObtenerCitaPorIdLN : IObtenerCitaPorIdLN
    {
        private readonly IObtenerCitaPorIdAD _obtenerAD;

        public ObtenerCitaPorIdLN()
        {
            _obtenerAD = new ObtenerCitaPorIdAD();
        }

        public CitaDto Obtener(int idCita)
        {
            return _obtenerAD.Obtener(idCita);
        }
    }
}