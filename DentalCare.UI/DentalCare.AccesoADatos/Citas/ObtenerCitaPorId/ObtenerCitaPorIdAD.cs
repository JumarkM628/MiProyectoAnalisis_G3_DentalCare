using DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerCitaPorId;
using DentalCare.Abstraccion.Modelo.Citas;
using System.Linq;

namespace DentalCare.AccesoADatos.Citas.ObtenerCitaPorId
{
    public class ObtenerCitaPorIdAD : IObtenerCitaPorIdAD
    {
        private readonly Contexto _contexto;

        public ObtenerCitaPorIdAD()
        {
            _contexto = new Contexto();
        }

        public CitaDto Obtener(int idCita)
        {
            // Paso 1: traer datos primitivos a memoria
            var rawData = (
                from cita in _contexto.Citas
                where cita.IdCita == idCita

                join motivo in _contexto.MotivosCita
                    on cita.IdMotivo equals motivo.IdMotivo into motivoGrupo
                from motivo in motivoGrupo.DefaultIfEmpty()

                join cancelacion in _contexto.MotivoCancelacionCita
                    on cita.IdCancelacion equals cancelacion.IdCancelacion into cancelGrupo
                from cancelacion in cancelGrupo.DefaultIfEmpty()

                join estado in _contexto.Estados
                    on cita.IdEstado equals estado.IdEstado

                // Paciente via FIDE_USUARIO_CITA_TB
                join uc in _contexto.UsuarioCitas
                    on cita.IdCita equals uc.IdCita into ucGrupo
                from uc in ucGrupo.DefaultIfEmpty()

                join paciente in _contexto.Usuarios
                    on uc.IdUsuario equals paciente.IdUsuario into pacienteGrupo
                from paciente in pacienteGrupo.DefaultIfEmpty()

                join cedula in _contexto.Cedulas
                    on (paciente != null ? paciente.IdUsuario : 0) equals cedula.IdUsuario into cedulaGrupo
                from cedula in cedulaGrupo.DefaultIfEmpty()

                    // Doctor
                join doctor in _contexto.Usuarios
                    on cita.IdDoctor equals doctor.IdUsuario into doctorGrupo
                from doctor in doctorGrupo.DefaultIfEmpty()

                select new
                {
                    cita.IdCita,
                    cita.Fecha,
                    cita.Hora,
                    cita.IdMotivo,
                    cita.IdEstado,
                    cita.IdDoctor,
                    cita.FechaCancelacion,
                    NombreMotivo = motivo != null ? motivo.Descripcion : "Sin motivo",
                    MotivoCancelacion = cancelacion != null ? cancelacion.Descripcion : "",
                    NombreEstado = estado.NombreEstado,
                    NombrePaciente = paciente != null
                        ? paciente.Nombre + " " + paciente.PrimerApellido
                        : "Sin paciente",
                    CedulaPaciente = cedula != null ? cedula.NumeroCedula : "",
                    NombreDoctor = doctor != null
                        ? doctor.Nombre + " " + doctor.PrimerApellido
                        : "Sin asignar"
                }
            ).FirstOrDefault();

            if (rawData == null) return null;

            return new CitaDto
            {
                IdCita = rawData.IdCita,
                Fecha = rawData.Fecha,
                Hora = rawData.Hora,
                HoraString = rawData.Hora.HasValue
                    ? rawData.Hora.Value.ToString(@"hh\:mm")
                    : "—",
                IdMotivo = rawData.IdMotivo,
                NombreMotivo = rawData.NombreMotivo,
                IdEstado = rawData.IdEstado,
                NombreEstado = rawData.NombreEstado,
                IdDoctor = rawData.IdDoctor ?? 0,
                NombreDoctor = rawData.NombreDoctor,
                NombrePaciente = rawData.NombrePaciente,
                CedulaPaciente = rawData.CedulaPaciente,
                FechaModificacion = rawData.FechaCancelacion
            };
        }
    }
}