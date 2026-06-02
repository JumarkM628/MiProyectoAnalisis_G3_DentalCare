using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.ReabrirExpediente
{
    public interface IReabrirExpedienteLN
    {
        string ReabrirExpediente(int id, string nombreDoctora);
    }
}
