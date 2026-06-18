using DentalCare.Abstraccion.AccesoADatos.Citas.AgregarCita;
using DentalCare.Abstraccion.Modelo.Citas;
using DentalCare.AccesoADatos.Entidades.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Citas.AgregarCita
{
    public class AgregarCitaAD : IAgregarCitaAD
    {
        private readonly Contexto _contexto;

        public AgregarCitaAD()
        {
            _contexto = new Contexto();
        }

        public void Agregar(CitaDto dto, int idPaciente)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    // ID directo desde SQL
                    int nuevoId = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_CITA), 0) + 1 FROM FIDE_CITAS_TB")
                        .FirstOrDefault();

                    // Insert directo sin pasar por EF
                    _contexto.Database.ExecuteSqlCommand(
                        @"INSERT INTO FIDE_CITAS_TB 
                  (ID_CITA, FECHA, HORA, ID_MOTIVO, ID_CANCELACION, ID_ESTADO, ID_DOCTOR)
                  VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6)",
                        nuevoId,
                        dto.Fecha.Value,
                        dto.Hora.Value,
                        dto.IdMotivo,
                        1,              // Sin cancelación por defecto
                        dto.IdEstado,
                        dto.IdDoctor);

                    // Vincular paciente
                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_USUARIO_CITA_TB (ID_USUARIO, ID_CITA) VALUES (@p0, @p1)",
                        idPaciente, nuevoId);

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        // Escenario 4: verificar que el paciente exista por cédula
        public bool ExistePaciente(string cedulaPaciente)
        {
            return _contexto.Cedulas
                .Any(c => c.NumeroCedula == cedulaPaciente);
        }

        // Escenario 2: verificar horario ocupado para ese doctor
        public bool HorarioOcupado(int idDoctor, DateTime fecha, TimeSpan hora)
        {
            return _contexto.Citas
                .Any(c => c.IdDoctor == idDoctor
                       && c.Fecha == fecha
                       && c.Hora == hora
                       && c.IdEstado == 1); // Solo citas activas
        }
    }
}
