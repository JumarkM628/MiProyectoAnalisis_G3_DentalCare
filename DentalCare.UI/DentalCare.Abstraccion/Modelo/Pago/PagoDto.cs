using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Pagos
{
    public class PagoDto
    {
        // ================================================================
        // SOLO LECTURA (historial)
        // ================================================================
        [Display(Name = "ID Pago")]
        public int IdContabilidad { get; set; }

        [Display(Name = "Paciente")]
        public string NombrePaciente { get; set; }

        [Display(Name = "Cita")]
        public int IdCita { get; set; }

        [Display(Name = "Método de Pago")]
        public string NombreMetodoPago { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Fecha de Pago")]
        public DateTime? FechaPago { get; set; }

        // ================================================================
        // FORMULARIO (registrar pago)
        // ================================================================

        // Paso 1: paciente y cita
        [Display(Name = "Paciente")]
        public int IdPacienteForm { get; set; }

        [Required(ErrorMessage = "La cita es obligatoria.")]
        [Display(Name = "ID de la Cita")]
        public int IdCitaForm { get; set; }

        // Paso 2: monto calculado desde tratamientos
        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, 9999999.99, ErrorMessage = "El monto debe ser mayor a cero.")]
        [Display(Name = "Monto (₡)")]
        public decimal Monto { get; set; }

        // Paso 3: método de pago
        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [Display(Name = "Método de Pago")]
        public int IdMetodoPago { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        // ================================================================
        // DROPDOWNS
        // ================================================================
        public IEnumerable<SelectListItem> ListaMetodosPago { get; set; }
        public IEnumerable<SelectListItem> ListaEstados { get; set; }
        public IEnumerable<SelectListItem> ListaPacientes { get; set; }
        public IEnumerable<SelectListItem> ListaCitas { get; set; }
        public IEnumerable<SelectListItem> ListaCatalogo { get; set; }
    }
}