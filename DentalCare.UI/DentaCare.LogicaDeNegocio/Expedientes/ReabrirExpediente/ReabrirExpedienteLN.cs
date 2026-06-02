using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Expediente.ReabrirExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.ReabrirExpediente;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentaCare.LogicaDeNegocio.Expedientes.ReabrirExpediente
{
    public class ReabrirExpedienteLN : IReabrirExpedienteLN
    {
        private readonly IReabrirExpedienteAD _reabrirExpedienteAD;

        public ReabrirExpedienteLN(IReabrirExpedienteAD reabrirExpedienteAD)
        {
            _reabrirExpedienteAD = reabrirExpedienteAD;
        }
        public ExpedienteDto ObtenerExpedientePorId(int id)
        {
            return _reabrirExpedienteAD.ObtenerExpedientePorId(id);
        }
        public string ReabrirExpediente(int id, string nombreDoctora)
        {
            var expediente = _reabrirExpedienteAD.ObtenerExpedientePorId(id);

            if (expediente == null)
                return "El expediente no fue encontrado.";

            if (expediente.IdEstado == 1)
                return "El expediente ya se encuentra activo.";

            bool reabierto = _reabrirExpedienteAD.ReabrirExpediente(id);

            if (!reabierto)
                return "Ocurrió un error al reabrir el expediente.";

            var bitacora = new BitacoraDto
            {
                Modulo = "Expediente",
                Accion = "Reapertura",
                Descripcion = $"Se reabrió el expediente ID {id} del paciente {expediente.NombrePaciente}.",
                NombreUsuario = nombreDoctora,
                FechaHora = DateTime.Now
            };

            _reabrirExpedienteAD.RegistrarReaperturaEnBitacora(bitacora);

            return null;
        }
    }
}
