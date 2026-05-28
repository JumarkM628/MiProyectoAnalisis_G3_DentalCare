using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Expedientes
{
    public class ExpedienteDto
    {

        [Display(Name = "ID Expediente")]
        public int IdExpediente { get; set; }

        [Display(Name = "ID Paciente")]
        public int IdPaciente { get; set; }

        [Display(Name = "Paciente")]
        public string NombrePaciente { get; set; }

        [Display(Name = "Cédula Paciente")]
        public string CedulaPaciente { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateTime? FechaCreacion { get; set; }

        [Required(ErrorMessage = "La identificación del paciente es obligatoria.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La identificación solo puede contener números.")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [Display(Name = "Identificación del Paciente")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El objetivo es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El objetivo solo puede contener letras.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        [Display(Name = "Objetivo")]
        public string Objetivo { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "La descripción solo puede contener letras.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Las alternativas son obligatorias.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Las alternativas solo pueden contener letras.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        [Display(Name = "Alternativas")]
        public string Alternativas { get; set; }

        [Required(ErrorMessage = "Las consecuencias son obligatorias.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Las consecuencias solo pueden contener letras.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        [Display(Name = "Consecuencias")]
        public string Consecuencias { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "El campo otro solo puede contener letras.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Display(Name = "Otro")]
        public string Otro { get; set; }

        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        public IEnumerable<SelectListItem> ListaEstados { get; set; }
    }
}
