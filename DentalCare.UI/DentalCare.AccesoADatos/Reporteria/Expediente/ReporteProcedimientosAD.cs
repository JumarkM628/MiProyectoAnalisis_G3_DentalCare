using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Expediente;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Expediente
{
    public class ReporteProcedimientosAD : IReporteProcedimientosAD
    {
        private readonly Contexto _contexto;

        public ReporteProcedimientosAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        private List<ReporteProcedimientosDto> EjecutarQuery(
            int idExpediente, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var consulta =
                from expediente in _contexto.Expedientes
                where expediente.IdExpediente == idExpediente

                join procedimiento in _contexto.Procedimientos
                    on expediente.IdProcedimiento equals procedimiento.ID_PROCEDIMIENTO

                join tratamiento in _contexto.PlanesTratamiento
                    on procedimiento.ID_TRATAMIENTO equals tratamiento.IdTratamiento
                    into tratamientoJoin
                from tratamiento in tratamientoJoin.DefaultIfEmpty()

                join cita in _contexto.Citas
                    on procedimiento.ID_CITA equals cita.IdCita
                    into citaJoin
                from cita in citaJoin.DefaultIfEmpty()

                join usuarioCita in _contexto.UsuarioCitas
                    on (cita != null ? cita.IdCita : 0) equals usuarioCita.IdCita
                    into usuarioCitaJoin
                from usuarioCita in usuarioCitaJoin.DefaultIfEmpty()

                join usuario in _contexto.Usuarios
                    on (usuarioCita != null ? usuarioCita.IdUsuario : 0) equals usuario.IdUsuario
                    into usuarioJoin
                from usuario in usuarioJoin.DefaultIfEmpty()

                join estado in _contexto.Estados
                    on procedimiento.ID_ESTADO equals estado.IdEstado
                    into estadoJoin
                from estado in estadoJoin.DefaultIfEmpty()

                where (!fechaInicio.HasValue || procedimiento.FECHA >= fechaInicio.Value)
                   && (!fechaFin.HasValue || procedimiento.FECHA <= fechaFin.Value)

                orderby procedimiento.FECHA descending

                select new ReporteProcedimientosDto
                {
                    IdProcedimiento = procedimiento.ID_PROCEDIMIENTO,
                    IdExpediente = expediente.IdExpediente,
                    NombrePaciente = usuario == null
                        ? "—"
                        : usuario.Nombre + " " +
                          usuario.PrimerApellido + " " +
                          usuario.SegundoApellido,
                    DescripcionProcedimiento = procedimiento.DESCRIPCION,
                    FechaProcedimiento = procedimiento.FECHA,
                    Observaciones = procedimiento.OBSERVACIONES,
                    PlanTratamiento = tratamiento == null
                        ? "—"
                        : tratamiento.Descripcion,
                    EstadoProcedimiento = estado == null ? "—" : estado.NombreEstado
                };

            return consulta.ToList();
        }
        public List<ReporteProcedimientosDto> ObtenerProcedimientosPorExpediente(int idExpediente)
        {
            return EjecutarQuery(idExpediente, null, null);
        }
        public List<ReporteProcedimientosDto> ObtenerProcedimientosPorExpedienteFiltrado(
            int idExpediente, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return EjecutarQuery(idExpediente, fechaInicio, fechaFin);
        }
        public List<ExpedienteItemDto> ObtenerExpedientes()
        {
            var consulta =
                from expediente in _contexto.Expedientes

                join usuarioExpediente in _contexto.UsuarioExpedientes
                    on expediente.IdExpediente equals usuarioExpediente.IdExpediente
                    into ueJoin
                from usuarioExpediente in ueJoin.DefaultIfEmpty()

                join usuario in _contexto.Usuarios
                    on (usuarioExpediente != null ? usuarioExpediente.IdUsuario : 0)
                    equals usuario.IdUsuario
                    into usuarioJoin
                from usuario in usuarioJoin.DefaultIfEmpty()

                where expediente.IdEstado == 1

                select new ExpedienteItemDto
                {
                    IdExpediente = expediente.IdExpediente,
                    NombrePaciente = usuario == null
                        ? "Expediente " + expediente.IdExpediente
                        : usuario.Nombre + " " +
                          usuario.PrimerApellido + " " +
                          usuario.SegundoApellido
                };

            return consulta.ToList();
        }
    }
}
