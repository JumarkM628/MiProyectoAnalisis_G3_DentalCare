using DentalCare.Abstraccion.AccesoADatos.Expediente.CerrarExpediente;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentalCare.AccesoADatos.Expedientes.CerrarExpediente
{
    public class CerrarExpedienteAD : ICerrarExpedienteAD
    {
        private readonly Contexto _contexto;

        public CerrarExpedienteAD(Contexto contexto)
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
                FechaCreacion = entidad.FechaDeCreacion,
                IdEstado = entidad.IdEstado,
                NombreEstado = entidad.IdEstado == 1 ? "Activo" : "Inactivo"
            };
        }

        public bool CerrarExpediente(int id)
        {
            var entidad = _contexto.Expedientes.Find(id);
            if (entidad == null) return false;

            entidad.IdEstado = 2;
            _contexto.SaveChanges();
            // El trigger trg_Expediente_Update registra el cambio automáticamente
            return true;
        }
    }
}