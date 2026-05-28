using DentalCare.Abstraccion.Modelo.Expedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Expediente.ObtenerTodosLosExpedientes
{
    public interface IObtenerTodosLosExpedientesAD
    {
        List<ExpedienteDto> Obtener();
    }
}
