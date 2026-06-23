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

        public const string ESTADO_ACTIVO = "Activo";
        public const string ESTADO_CANCELADA = "Cancelada";
        public const string ESTADO_RECHAZADA = "Rechazada";
        public const string ESTADO_PENDIENTE = "Pendiente";
        public const string ESTADO_CONFIRMADA = "Confirmada";
        public const string ESTADO_ASISTIDA = "Asistida";     
        public const string ESTADO_AUSENTE = "Ausente";        
        public const string ESTADO_FINALIZADA = "Finalizada";

        public CambiarEstadoCitaAD()
        {
            _contexto = new Contexto();
        }


        private int ObtenerIdEstado(string nombreEstado)
        {
            var estado = _contexto.Estados.FirstOrDefault(e => e.NombreEstado == nombreEstado);
            if (estado == null)
                throw new Exception($"El estado '{nombreEstado}' no existe en la base de datos.");
            return estado.IdEstado;
        }

        // Cambiar estado usando el nombre, no el ID
        public void CambiarEstado(int idCita, string nombreEstado, int idMotivoCancelacion = 1)
        {
            int idEstado = ObtenerIdEstado(nombreEstado);
            using (var trans = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var cita = _contexto.Citas.First(c => c.IdCita == idCita);
                    cita.IdEstado = idEstado;
                    cita.IdCancelacion = idMotivoCancelacion;
                    cita.Fecha = DateTime.Now;
                    _contexto.SaveChanges();
                    trans.Commit();
                }
                catch { trans.Rollback(); throw; }
            }
        }

        // Registrar asistencia (actualiza hora_inicio)
        public void RegistrarAsistencia(int idCita, TimeSpan horaInicio)
        {
            int idEstado = ObtenerIdEstado(ESTADO_ASISTIDA);
            using (var trans = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var cita = _contexto.Citas.First(c => c.IdCita == idCita);
                    cita.IdEstado = idEstado;
                    cita.Hora = horaInicio;
                    cita.Fecha = DateTime.Now;
                    _contexto.SaveChanges();
                    trans.Commit();
                }
                catch { trans.Rollback(); throw; }
            }
        }

        // Registrar ausencia (no guarda hora)
        public void RegistrarAusencia(int idCita)
        {
            int idEstado = ObtenerIdEstado(ESTADO_AUSENTE);
            using (var trans = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var cita = _contexto.Citas.First(c => c.IdCita == idCita);
                    cita.IdEstado = idEstado;
                    cita.Fecha = DateTime.Now;
                    _contexto.SaveChanges();
                    trans.Commit();
                }
                catch { trans.Rollback(); throw; }
            }
        }

        // Registrar finalización (actualiza hora_fin)
        public void RegistrarFinalizacion(int idCita, TimeSpan horaFin)
        {
            int idEstado = ObtenerIdEstado(ESTADO_FINALIZADA);
            using (var trans = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var cita = _contexto.Citas.First(c => c.IdCita == idCita);
                    cita.IdEstado = idEstado;
                    cita.Hora = horaFin;
                    cita.Fecha = DateTime.Now;
                    _contexto.SaveChanges();
                    trans.Commit();
                }
                catch { trans.Rollback(); throw; }
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
