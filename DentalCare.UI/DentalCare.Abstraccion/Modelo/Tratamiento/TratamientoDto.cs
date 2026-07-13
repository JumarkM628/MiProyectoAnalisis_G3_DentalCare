using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Tratamientos
{
    public class TratamientoDto
    {
        // ================================================================
        // SOLO LECTURA (resumen por cita - Escenario 5)
        // ================================================================

        [Display(Name = "ID Tratamiento")]
        public int IdTratamiento { get; set; }

        [Display(Name = "Cita")]
        public int IdCita { get; set; }

        [Display(Name = "Descripción")]
        public string DescripcionMostrar { get; set; }

        [Display(Name = "Costo")]
        public decimal MontoMostrar { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha Fin")]
        public DateTime? FechaFin { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        // Escenario 4: total acumulado de todos los tratamientos de la cita
        [Display(Name = "Total a Pagar")]
        public decimal TotalCita { get; set; }

        // ================================================================
        // FORMULARIO (registrar tratamiento - Escenarios 1, 2, 3)
        // ================================================================

        [Required(ErrorMessage = "El ID de la cita es obligatorio.")]
        [Display(Name = "ID de la Cita")]
        public int IdCitaForm { get; set; }

        [Required(ErrorMessage = "La descripción del tratamiento es obligatoria.")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Descripción del Tratamiento")]
        public string Descripcion { get; set; }

        // Escenario 2: costo obligatorio
        [Required(ErrorMessage = "El costo del tratamiento es obligatorio.")]
        [Range(0.01, 9999999.99, ErrorMessage = "El costo debe ser mayor a cero.")]
        [Display(Name = "Costo (₡)")]
        public decimal Monto { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime? FechaInicioForm { get; set; }

        [Display(Name = "Fecha Fin")]
        public DateTime? FechaFinForm { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        // ================================================================
        // DROPDOWNS
        // ================================================================
        public IEnumerable<SelectListItem> ListaEstados { get; set; }
    }
}