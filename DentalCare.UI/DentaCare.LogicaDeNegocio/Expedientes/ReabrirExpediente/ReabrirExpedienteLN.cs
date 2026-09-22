using DentalCare.Abstraccion.AccesoADatos.Expediente.ReabrirExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.ReabrirExpediente;
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

            // El trigger trg_Expediente_Update registra la reapertura automáticamente
            return null;
        }
    }
}