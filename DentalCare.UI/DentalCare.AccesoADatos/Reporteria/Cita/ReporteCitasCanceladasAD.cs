using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Cita;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Cita
{
    public class ReporteCitasCanceladasAD : IReporteCitasCanceladasAD
    {
        private readonly Contexto _contexto;

        public ReporteCitasCanceladasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<ReporteCitasCanceladasDto> ObtenerCitasCanceladas(
            DateTime? fechaInicio, DateTime? fechaFin)
        {
            var consulta =
                from cita in _contexto.Citas
                join estado in _contexto.Estados
                    on cita.IdEstado equals estado.IdEstado

                join motivoCita in _contexto.MotivosCita
                    on cita.IdMotivo equals motivoCita.IdMotivo

                join motivoCancel in _contexto.MotivoCancelacionCita
                    on cita.IdCancelacion equals motivoCancel.IdCancelacion into cancelJoin
                from motivoCancel in cancelJoin.DefaultIfEmpty()

                join usuarioCita in _contexto.UsuarioCitas
                    on cita.IdCita equals usuarioCita.IdCita into usuarioJoin
                from usuarioCita in usuarioJoin.DefaultIfEmpty()

                join usuario in _contexto.Usuarios
                    on usuarioCita.IdUsuario equals usuario.IdUsuario into usuarioFinal
                from usuario in usuarioFinal.DefaultIfEmpty()

                where estado.NombreEstado == "Cancelada"
                   && (!fechaInicio.HasValue || cita.Fecha >= fechaInicio.Value)
                   && (!fechaFin.HasValue || cita.Fecha <= fechaFin.Value)

                orderby cita.Fecha descending

                select new ReporteCitasCanceladasDto
                {
                    IdCita = cita.IdCita,
                    IdUsuario = usuarioCita == null ? 0 : usuarioCita.IdUsuario,
                    NombrePaciente = usuario == null
                        ? "—"
                        : usuario.Nombre + " " +
                          usuario.PrimerApellido + " " +
                          usuario.SegundoApellido,
                    FechaCita = cita.Fecha,
                    HoraCita = cita.Hora,
                    MotivoCita = motivoCita.Descripcion,
                    EstadoCita = estado.NombreEstado,
                    MotivoCancelacion = motivoCancel == null
                        ? "—"
                        : motivoCancel.Descripcion,

                    FechaCancelacion = null
                };

            return consulta.ToList();
        }
    }
}