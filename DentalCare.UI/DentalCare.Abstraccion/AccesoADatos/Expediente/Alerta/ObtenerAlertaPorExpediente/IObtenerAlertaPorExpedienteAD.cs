using DentalCare.Abstraccion.Modelo.Expedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Expediente.Alerta.ObtenerAlertaPorExpediente
{
    public interface IObtenerAlertaPorExpedienteAD
    {
        ExpedienteDetalleDto Obtener(int idExpediente);
    }
}
