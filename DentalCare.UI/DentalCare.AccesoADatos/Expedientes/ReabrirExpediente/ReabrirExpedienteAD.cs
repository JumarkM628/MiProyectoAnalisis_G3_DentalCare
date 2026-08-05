using DentalCare.Abstraccion.AccesoADatos.Expediente.ReabrirExpediente;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentalCare.AccesoADatos.Expedientes.ReabrirExpediente
{
    public class ReabrirExpedienteAD : IReabrirExpedienteAD
    {
        private readonly Contexto _contexto;

        public ReabrirExpedienteAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public ExpedienteDto ObtenerExpedientePorId(int id)
        {
            var entidad = _contexto.Expedientes.Find(id);
            if (entidad == null) return null;

            return new ExpedienteDto
            {
                IdExpediente = entidad.IdExpediente,
                IdEstado = entidad.IdEstado,
                FechaCreacion = entidad.FechaDeCreacion
            };
        }

        public bool ReabrirExpediente(int id)
        {
            var entidad = _contexto.Expedientes.Find(id);
            if (entidad == null) return false;

            entidad.IdEstado = 1;
            entidad.FechaDeCreacion = null;
            _contexto.SaveChanges();
            // El trigger trg_Expediente_Update registra el cambio automáticamente
            return true;
        }
    }
}