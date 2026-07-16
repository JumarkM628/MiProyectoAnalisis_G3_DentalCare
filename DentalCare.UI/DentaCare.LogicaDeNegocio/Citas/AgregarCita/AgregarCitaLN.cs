using DentalCare.Abstraccion.AccesoADatos.Citas.AgregarCita;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.AgregarCita;
using DentalCare.Abstraccion.Modelo.Citas;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Citas.AgregarCita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Citas.AgregarCita
{
    public class AgregarCitaLN : IAgregarCitaLN
    {
        private readonly IAgregarCitaAD _agregarAD;
        private readonly Contexto _contexto;

        public AgregarCitaLN()
        {
            _agregarAD = new AgregarCitaAD();
            _contexto = new Contexto();
        }

        public string Agregar(CitaDto dto)
        {
            // Convertir HoraString a TimeSpan
            if (!string.IsNullOrEmpty(dto.HoraString) &&
                System.TimeSpan.TryParse(dto.HoraString, out var hora))
            {
                dto.Hora = hora;
            }
            else
            {
                return "El formato de la hora no es válido.";
            }

            // Escenario 4: paciente no registrado
            if (!_agregarAD.ExistePaciente(dto.CedulaPaciente))
                return "El paciente no está registrado en el sistema. Por favor registrelo antes de agendar una cita.";

            // Escenario 2: horario ocupado para ese doctor
            if (_agregarAD.HorarioOcupado(dto.IdDoctor, dto.Fecha.Value, dto.Hora.Value))
                return "El doctor ya tiene una cita agendada en esa fecha y hora. Por favor seleccione otro horario.";

            // Obtener IdUsuario del paciente por cédula
            int idPaciente = _contexto.Cedulas
                .Where(c => c.NumeroCedula == dto.CedulaPaciente)
                .Select(c => c.IdUsuario)
                .First();

            _agregarAD.Agregar(dto, idPaciente);
            return null;
        }
    }
}
