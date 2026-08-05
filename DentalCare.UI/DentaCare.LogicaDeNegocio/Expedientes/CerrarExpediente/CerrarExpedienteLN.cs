using DentalCare.Abstraccion.AccesoADatos.Expediente.CerrarExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CerrarExpediente;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentaCare.LogicaDeNegocio.Expedientes.CerrarExpediente
{
    public class CerrarExpedienteLN : ICerrarExpedienteLN
    {
        private readonly ICerrarExpedienteAD _cerrarExpedienteAD;

        public CerrarExpedienteLN(ICerrarExpedienteAD cerrarExpedienteAD)
        {
            _cerrarExpedienteAD = cerrarExpedienteAD;
        }

        public ExpedienteDto ObtenerExpedientePorId(int id)
        {
            return _cerrarExpedienteAD.ObtenerExpedientePorId(id);
        }

        public string CerrarExpediente(int id, string nombreDoctora)
        {
            var expediente = _cerrarExpedienteAD.ObtenerExpedientePorId(id);

            if (expediente == null)
                return "El expediente no fue encontrado.";

            if (expediente.IdEstado == 2)
                return "El expediente ya se encuentra cerrado y no puede modificarse.";

            bool cerrado = _cerrarExpedienteAD.CerrarExpediente(id);
            if (!cerrado)
                return "Ocurrió un error al cerrar el expediente.";

            // El trigger trg_Expediente_Update registra el cierre automáticamente
            return null;
        }
    }
}