using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentalCare.Abstraccion.AccesoADatos.Expediente.ReabrirExpediente
{
    public interface IReabrirExpedienteAD
    {
        ExpedienteDto ObtenerExpedientePorId(int id);
        bool ReabrirExpediente(int id);
        void RegistrarReaperturaEnBitacora(BitacoraDto bitacora);
    }
}
