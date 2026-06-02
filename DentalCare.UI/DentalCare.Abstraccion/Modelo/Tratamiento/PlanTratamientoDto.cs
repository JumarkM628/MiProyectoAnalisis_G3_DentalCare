using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Tratamiento
{
    public class PlanTratamientoDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; }

        [Required(ErrorMessage = "El diagnóstico es obligatorio.")]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "El tratamiento es obligatorio.")]
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public DateTime FechaInicio { get; set; }

    }
}

