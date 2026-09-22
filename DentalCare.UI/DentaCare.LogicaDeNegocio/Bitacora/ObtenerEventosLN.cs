using DentalCare.Abstraccion.AccesoADatos.Bitacora;
using DentalCare.Abstraccion.LogicaDeNegocio.Bitacora;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.AccesoADatos.Bitacora.ObtenerEventos;
using System.Collections.Generic;

namespace DentalCare.LogicaDeNegocio.Bitacora.ObtenerEventos
{
    public class ObtenerEventosLN : IObtenerEventosLN
    {
        private readonly IObtenerEventosAD _obtenerAD;

        public ObtenerEventosLN()
        {
            _obtenerAD = new ObtenerEventosAD();
        }

        public List<EventoDto> Obtener()
        {
            return _obtenerAD.Obtener();
        }
    }
}