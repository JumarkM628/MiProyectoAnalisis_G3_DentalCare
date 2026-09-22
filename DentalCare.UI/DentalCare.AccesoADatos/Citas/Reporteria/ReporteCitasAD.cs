using System;
using System.Collections.Generic;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Citas.Reporteria;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Citas.Reporteria
{
    public class ReporteCitasAD : IReporteCitasAD
    {
        private readonly Contexto _contexto;

        public ReporteCitasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<CitaReporteDto> ObtenerPorPeriodo(DateTime desde, DateTime hasta)
        {
            var raw = (from cita in _contexto.Citas
                       join uc in _contexto.UsuarioCitas on cita.IdCita equals uc.IdCita into ucGrupo
                       from uc in ucGrupo.DefaultIfEmpty()
                       join paciente in _contexto.Usuarios on uc.IdUsuario equals paciente.IdUsuario into pacienteGrupo
                       from paciente in pacienteGrupo.DefaultIfEmpty()
                       join doctor in _contexto.Usuarios on cita.IdDoctor equals doctor.IdUsuario into doctorGrupo
                       from doctor in doctorGrupo.DefaultIfEmpty()
                       join motivo in _contexto.MotivosCita on cita.IdMotivo equals motivo.IdMotivo
                       join estado in _contexto.Estados on cita.IdEstado equals estado.IdEstado
                       join motCancel in _contexto.MotivoCancelacionCita on cita.IdCancelacion equals motCancel.IdCancelacion into motCancelGrp
                       from motCancel in motCancelGrp.DefaultIfEmpty()
                       where cita.Fecha >= desde && cita.Fecha <= hasta
                       select new
                       {
                           cita.IdCita,
                           cita.Fecha,
                           cita.Hora,
                           NombrePaciente = paciente != null ? paciente.Nombre + " " + paciente.PrimerApellido : "Sin asignar",
                           NombreDoctor = doctor != null ? doctor.Nombre + " " + doctor.PrimerApellido : "Sin asignar",
                           NombreMotivo = motivo.Descripcion,
                           NombreEstado = estado.NombreEstado
                       }).ToList();

            return raw.Select(r => new CitaReporteDto
            {
                IdCita = r.IdCita,
                Fecha = r.Fecha,
                HoraString = r.Hora.HasValue ? r.Hora.Value.ToString(@"hh\:mm") : "—",
                NombrePaciente = r.NombrePaciente,
                NombreDoctor = r.NombreDoctor,
                NombreMotivo = r.NombreMotivo,
                NombreEstado = r.NombreEstado
            }).ToList();
        }
    }
}
