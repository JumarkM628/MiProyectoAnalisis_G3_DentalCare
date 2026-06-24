using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Citas
{
    public class CitaDto
    {
        [Display(Name = "ID Cita")]
        public int IdCita { get; set; }

        [Display(Name = "Paciente")]
        public string NombrePaciente { get; set; }

        [Display(Name = "Doctor")]
        public string NombreDoctor { get; set; }

        [Display(Name = "Motivo")]
        public string NombreMotivo { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

         
        public string CorreoPaciente { get; set; }


        [Required(ErrorMessage = "La cédula del paciente es obligatoria.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La identificación solo puede contener números.")]
        [Display(Name = "Cédula del Paciente")]
        public string CedulaPaciente { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "La hora es obligatoria.")]
        [Display(Name = "Hora")]
        public TimeSpan? Hora { get; set; }

        // Hora como string para el input type="time" del formulario
        [Display(Name = "Hora")]
        public string HoraString { get; set; }

        [Required(ErrorMessage = "El doctor es obligatorio.")]
        [Display(Name = "Doctor")]
        public int IdDoctor { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio.")]
        [Display(Name = "Motivo de la Cita")]
        public int IdMotivo { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public IEnumerable<SelectListItem> ListaDoctores { get; set; }
        public IEnumerable<SelectListItem> ListaMotivos { get; set; }
        public IEnumerable<SelectListItem> ListaEstados { get; set; }

    }
}
