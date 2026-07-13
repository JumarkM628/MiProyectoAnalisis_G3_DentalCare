using DentalCare.Abstraccion.AccesoADatos.Tratamiento.Tratamientos;
using DentalCare.Abstraccion.LogicaDeNegocio.Tratamiento.ObtenerTratamientoLN;
using DentalCare.Abstraccion.Modelo.Tratamientos;
using DentalCare.AccesoADatos.Tratamientos.ObtenerTratamientosPorCita;
using System.Collections.Generic;

namespace DentalCare.LogicaDeNegocio.Tratamientos.ObtenerTratamientosPorCita
{
    public class ObtenerTratamientosPorCitaLN : IObtenerTratamientosPorCitaLN
    {
        private readonly IObtenerTratamientosPorCitaAD _obtenerAD;

        public ObtenerTratamientosPorCitaLN()
        {
            _obtenerAD = new ObtenerTratamientosPorCitaAD();
        }

        public List<TratamientoDto> Obtener(int idCita)
        {
            return _obtenerAD.Obtener(idCita);
        }
    }
}