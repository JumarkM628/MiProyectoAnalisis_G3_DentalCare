using DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerCitaPaciente;
using DentalCare.Abstraccion.Modelo.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Citas.ObtenerCitaPaciente
{
    public class ObtenerCitasPacienteAD : IObtenerCitasPacienteAD
    {
        private readonly Contexto _contexto;

        public ObtenerCitasPacienteAD()
        {
            _contexto = new Contexto();
        }

        /// <summary>
        /// Obtiene todas las citas del paciente logueado para su historial.
        /// </summary>
        public List<CitaDto> ObtenerPorPaciente(string aspNetUserId)
        {
            // Obtener IdUsuario del paciente logueado
            var idUsuario = _contexto.Usuarios
                .Where(u => u.ASPNET_USER_ID == aspNetUserId)
                .Select(u => u.IdUsuario)
                .FirstOrDefault();

            if (idUsuario == 0) return new List<CitaDto>();

            // Obtener IDs de citas del paciente
            var idsCitas = _contexto.UsuarioCitas
                .Where(uc => uc.IdUsuario == idUsuario)
                .Select(uc => uc.IdCita)
                .ToList();

            var rawData = (
                from cita in _contexto.Citas
                where idsCitas.Contains(cita.IdCita)

                join doctor in _contexto.Usuarios
                    on cita.IdDoctor equals doctor.IdUsuario into doctorGrupo
                from doctor in doctorGrupo.DefaultIfEmpty()

                join motivo in _contexto.MotivosCita
                    on cita.IdMotivo equals motivo.IdMotivo

                join estado in _contexto.Estados
                    on cita.IdEstado equals estado.IdEstado

                select new
                {
                    cita.IdCita,
                    cita.Fecha,
                    cita.IdDoctor,
                    cita.IdMotivo,
                    cita.IdEstado,
                    NombreDoctor = doctor != null
                        ? doctor.Nombre + " " + doctor.PrimerApellido
                        : "Sin asignar",
                    NombreMotivo = motivo.Descripcion,
                    NombreEstado = estado.NombreEstado
                }
            ).ToList();

            return rawData.Select(r => new CitaDto
            {
                IdCita = r.IdCita,
                Fecha = r.Fecha,
                IdDoctor = r.IdDoctor ?? 0,
                IdMotivo = r.IdMotivo,
                IdEstado = r.IdEstado,
                NombreDoctor = r.NombreDoctor,
                NombreMotivo = r.NombreMotivo,
                NombreEstado = r.NombreEstado
            })
            .OrderByDescending(c => c.Fecha)
            .ToList();
        }

        /// <summary>
        /// Escenario 1: obtiene citas activas/confirmadas que sean en las
        /// próximas 24 horas para enviarles recordatorio.
        /// </summary>
        public List<CitaDto> ObtenerCitasProximas24Horas()
        {
            var ahora = DateTime.Now;
            var limite = ahora.AddHours(24);
            var hoyFecha = ahora.Date;
            var manFecha = limite.Date;

            var rawData = (
                from cita in _contexto.Citas
                where (cita.IdEstado == 1 || cita.IdEstado == 6) // Activo o Confirmada
                   && cita.Fecha >= hoyFecha
                   && cita.Fecha <= manFecha

                join uc in _contexto.UsuarioCitas
                    on cita.IdCita equals uc.IdCita into ucGrupo
                from uc in ucGrupo.DefaultIfEmpty()

                join paciente in _contexto.Usuarios
                    on uc.IdUsuario equals paciente.IdUsuario into pacienteGrupo
                from paciente in pacienteGrupo.DefaultIfEmpty()

                join correo in _contexto.Correos
                    on paciente.IdUsuario equals correo.IdUsuario into correoGrupo
                from correo in correoGrupo.DefaultIfEmpty()

                join motivo in _contexto.MotivosCita
                    on cita.IdMotivo equals motivo.IdMotivo

                select new
                {
                    cita.IdCita,
                    cita.Fecha,
                    cita.IdEstado,
                    NombrePaciente = paciente != null
                        ? paciente.Nombre + " " + paciente.PrimerApellido
                        : "",
                    CorreoPaciente = correo != null ? correo.Correo : "",
                    NombreMotivo = motivo.Descripcion
                }
            ).ToList();

            return rawData.Select(r => new CitaDto
            {
                IdCita = r.IdCita,
                Fecha = r.Fecha,
                IdEstado = r.IdEstado,
                NombrePaciente = r.NombrePaciente,
                CorreoPaciente = r.CorreoPaciente,
                NombreMotivo = r.NombreMotivo
            }).ToList();
        }
    }
}
