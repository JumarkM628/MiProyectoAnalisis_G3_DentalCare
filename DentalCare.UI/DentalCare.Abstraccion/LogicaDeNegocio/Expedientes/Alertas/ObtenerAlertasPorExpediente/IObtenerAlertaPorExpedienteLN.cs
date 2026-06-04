using DentalCare.Abstraccion.Modelo.Expedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.Alertas.ObtenerAlertasPorExpediente
{
    public interface IObtenerAlertaPorExpedienteLN
    {
        ExpedienteDetalleDto Obtener(int idExpediente);
    }
}
