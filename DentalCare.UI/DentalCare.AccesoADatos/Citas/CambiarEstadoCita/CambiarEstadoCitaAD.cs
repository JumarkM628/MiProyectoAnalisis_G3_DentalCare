using DentalCare.Abstraccion.AccesoADatos.Citas.CambiarEstadoCita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Citas.CambiarEstadoCita
{
    public class CambiarEstadoCitaAD : ICambiarEstadoCitaAD
    {
        private readonly Contexto _contexto;

        public const int ESTADO_ACTIVO = 1;
        public const int ESTADO_CANCELADA = 3;
        public const int ESTADO_RECHAZADA = 4;
        public const int ESTADO_PENDIENTE = 5;
        public const int ESTADO_CONFIRMADA = 6;

        public CambiarEstadoCitaAD()
        {
            _contexto = new Contexto();
        }


        public void CambiarEstado(int idCita, int idEstado, int idMotivoCancelacion)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var cita = _contexto.Citas.First(c => c.IdCita == idCita);
                    cita.IdEstado = idEstado;
                    cita.IdCancelacion = idMotivoCancelacion;
                    _contexto.SaveChanges();
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public bool ExisteCita(int idCita)
        {
            return _contexto.Citas.Any(c => c.IdCita == idCita);
        }

        public string ObtenerCorreoPaciente(int idCita)
        {
            var idUsuario = _contexto.UsuarioCitas
                .Where(uc => uc.IdCita == idCita)
                .Select(uc => uc.IdUsuario)
                .FirstOrDefault();

            if (idUsuario == 0) return null;

            return _contexto.Correos
                .Where(c => c.IdUsuario == idUsuario)
                .Select(c => c.Correo)
                .FirstOrDefault();
        }
    }
}
