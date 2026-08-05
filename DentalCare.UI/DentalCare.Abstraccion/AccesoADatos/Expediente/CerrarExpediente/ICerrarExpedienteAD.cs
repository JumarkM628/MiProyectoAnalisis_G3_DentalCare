using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentalCare.Abstraccion.AccesoADatos.Expediente.CerrarExpediente
{
    public interface ICerrarExpedienteAD
    {
        ExpedienteDto ObtenerExpedientePorId(int id);
        bool CerrarExpediente(int id);
    }
}
