using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Usuario;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Usuario
{
    public class HistorialDoctoraAD : IHistorialDoctoraAD
    {
        private readonly Contexto _contexto;

        public HistorialDoctoraAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        private List<HistorialDoctoraDto> EjecutarQuery(
            string aspNetUserId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var consulta =
                from procedimiento in _contexto.Procedimientos
                join cita in _contexto.Citas
                    on procedimiento.ID_CITA equals cita.IdCita
                join doctora in _contexto.Usuarios
                    on cita.IdDoctor equals doctora.IdUsuario
                where doctora.ASPNET_USER_ID == aspNetUserId
                join tratamiento in _contexto.PlanesTratamiento
                    on procedimiento.ID_TRATAMIENTO equals tratamiento.IdTratamiento
                    into tratamientoJoin
                from tratamiento in tratamientoJoin.DefaultIfEmpty()
                join usuarioCita in _contexto.UsuarioCitas
                    on cita.IdCita equals usuarioCita.IdCita
                    into usuarioCitaJoin
                from usuarioCita in usuarioCitaJoin.DefaultIfEmpty()

                join paciente in _contexto.Usuarios
                    on usuarioCita.IdUsuario equals paciente.IdUsuario
                    into pacienteJoin
                from paciente in pacienteJoin.DefaultIfEmpty()
                join estado in _contexto.Estados
                    on procedimiento.ID_ESTADO equals estado.IdEstado
                    into estadoJoin
                from estado in estadoJoin.DefaultIfEmpty()
                where (!fechaInicio.HasValue || procedimiento.FECHA >= fechaInicio.Value)
                   && (!fechaFin.HasValue || procedimiento.FECHA <= fechaFin.Value)

                orderby procedimiento.FECHA descending

                select new HistorialDoctoraDto
                {
                    IdProcedimiento = procedimiento.ID_PROCEDIMIENTO,
                    NombrePaciente = paciente == null
                        ? "—"
                        : paciente.Nombre + " " +
                          paciente.PrimerApellido + " " +
                          paciente.SegundoApellido,
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
        public List<HistorialDoctoraDto> ObtenerHistorialPorDoctora(string aspNetUserId)
        {
            return EjecutarQuery(aspNetUserId, null, null);
        }
        public List<HistorialDoctoraDto> ObtenerHistorialPorDoctoraFiltrado(
            string aspNetUserId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return EjecutarQuery(aspNetUserId, fechaInicio, fechaFin);
        }
    }
}
