using DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerTodasLasCitas;
using DentalCare.Abstraccion.Modelo.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Citas.ObtenerTodasLasCitas
{
    public class ObtenerTodasLasCitasAD : IObtenerTodasLasCitasAD
    {
        private readonly Contexto _contexto;

        public ObtenerTodasLasCitasAD()
        {
            _contexto = new Contexto();
        }

        public List<CitaDto> Obtener()
        {
            var rawData = (
                from cita in _contexto.Citas
                join uc in _contexto.UsuarioCitas
                    on cita.IdCita equals uc.IdCita into ucGrupo
                from uc in ucGrupo.DefaultIfEmpty()

                join paciente in _contexto.Usuarios
                    on uc.IdUsuario equals paciente.IdUsuario into pacienteGrupo
                from paciente in pacienteGrupo.DefaultIfEmpty()
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
                    cita.Hora,
                    cita.IdDoctor,
                    cita.IdMotivo,
                    cita.IdEstado,
                    NombrePaciente = paciente != null
                        ? paciente.Nombre + " " + paciente.PrimerApellido
                        : "Sin asignar",
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
                Hora = r.Hora,
                HoraString = r.Hora.HasValue ? r.Hora.Value.ToString(@"hh\:mm") : "—",
                IdDoctor = r.IdDoctor ?? 0,
                IdMotivo = r.IdMotivo,
                IdEstado = r.IdEstado,
                NombrePaciente = r.NombrePaciente,
                NombreDoctor = r.NombreDoctor,
                NombreMotivo = r.NombreMotivo,
                NombreEstado = r.NombreEstado
            }).ToList();
        }
    }
}
