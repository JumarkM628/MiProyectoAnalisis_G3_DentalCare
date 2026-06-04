using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Expediente.CerrarExpediente;
using DentalCare.Abstraccion.AccesoADatos.Expediente.CrearExpediente;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Entidades.Bitacora;

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
            return true;
        }

        public void RegistrarCierreEnBitacora(BitacoraDto bitacora)
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

