using DentalCare.Abstraccion.AccesoADatos.Expediente.ObtenerTodosLosExpedientes;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.ObtenerTodosLosExpedientes;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Expedientes;
using DentalCare.AccesoADatos.Expedientes.ObtenerTodosLosExpedientes;
using System.Collections.Generic;
using System.Linq;

namespace DentalCare.LogicaDeNegocio.Expedientes.ObtenerTodosLosExpedientes
{
    public class ObtenerTodosLosExpedientesLN : IObtenerTodosLosExpedientesLN
    {
        private readonly IObtenerTodosLosExpedientesAD _obtenerAD;

        public ObtenerTodosLosExpedientesLN()
        {
            _obtenerAD = new ObtenerTodosLosExpedientesAD();
        }

        public List<ExpedienteDto> Obtener()
        {
            return _obtenerAD.Obtener()
                .OrderBy(e => e.NombrePaciente)
                .ToList();
        }
    }
}

