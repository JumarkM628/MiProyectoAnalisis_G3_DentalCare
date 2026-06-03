using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Alertas
{
    public class AlertaDto
    {


        [Display(Name = "ID Alerta")]
        public int IdAlerta { get; set; }

        [Display(Name = "ID Expediente")]
        public int IdExpediente { get; set; }

        [Display(Name = "Paciente")]
        public string NombrePaciente { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Nivel de Riesgo")]
        public string NivelRiesgoMostrar { get; set; }

        [Required(ErrorMessage = "Los antecedentes médicos son obligatorios.")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Antecedentes Médicos")]
        public string AntecedentesMedicos { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Alergias")]
        public string Alergias { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Medicación Actual")]
        public string MedicacionActual { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Cirugías Previas")]
        public string CirugiasPrevias { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Hábitos")]
        public string Habitos { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Antecedentes Odontológicos")]
        public string AntecedentesOdontologicos { get; set; }

        [Required(ErrorMessage = "El motivo de consulta es obligatorio.")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Motivo de Consulta")]
        public string MotivoConsulta { get; set; }

        [Required(ErrorMessage = "El diagnóstico inicial es obligatorio.")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Diagnóstico Inicial")]
        public string DiagnosticoInicial { get; set; }

        [Required(ErrorMessage = "El plan de tratamiento es obligatorio.")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Plan de Tratamiento")]
        public string PlanTratamiento { get; set; }

        [Required(ErrorMessage = "El nivel de riesgo es obligatorio.")]
        [Display(Name = "Nivel de Riesgo")]
        public string NivelRiesgo { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        // ================================================================
        // DROPDOWNS
        // ================================================================
        public IEnumerable<SelectListItem> ListaEstados { get; set; }
        public IEnumerable<SelectListItem> ListaNivelesRiesgo { get; set; }
    }
}
