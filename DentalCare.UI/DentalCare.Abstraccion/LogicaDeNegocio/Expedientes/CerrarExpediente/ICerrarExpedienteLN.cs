using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CerrarExpediente
{
    public interface ICerrarExpedienteLN
    {
        ExpedienteDto ObtenerExpedientePorId(int id);
        string CerrarExpediente(int id, string nombreDoctora);
    }
}
