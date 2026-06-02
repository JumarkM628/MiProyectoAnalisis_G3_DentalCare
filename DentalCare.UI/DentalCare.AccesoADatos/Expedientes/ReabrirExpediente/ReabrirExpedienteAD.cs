using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Expediente.ReabrirExpediente;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Entidades.Bitacora;

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

            if (entidad == null)
                return null;

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

            if (entidad == null)
                return false;

            entidad.IdEstado = 1; 
            entidad.FechaDeCreacion = null;

            _contexto.SaveChanges();

            return true;
        }

        public void RegistrarReaperturaEnBitacora(BitacoraDto bitacora)
        {
            var entidad = new BitacoraEntidad
            {
                Modulo = bitacora.Modulo,
                Accion = bitacora.Accion,
                Descripcion = bitacora.Descripcion,
                NombreUsuario = bitacora.NombreUsuario,
                FechaHora = bitacora.FechaHora
            };

            _contexto.Bitacoras.Add(entidad);
            _contexto.SaveChanges();
        }
    }
}
