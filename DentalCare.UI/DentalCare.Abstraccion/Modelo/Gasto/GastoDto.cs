using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Gasto
{
    public class GastoDto
    {
        // ================================================================
        // SOLO LECTURA (reporte financiero - Escenario 5)
        // ================================================================

        [Display(Name = "ID Gasto")]
        public int IdGasto { get; set; }

        [Display(Name = "Descripción")]
        public string DescripcionMostrar { get; set; }

        // Escenario 3: tipo/categoría extraído del campo concatenado
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }

        [Display(Name = "Monto")]
        public decimal MontoMostrar { get; set; }

        [Display(Name = "Fecha")]
        public DateTime? FechaMostrar { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        // ================================================================
        // FORMULARIO (registrar gasto - Escenarios 1, 2, 3)
        // ================================================================

        // Escenario 3: categoría como campo separado, se concatena en DESCRIPCION
        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [Display(Name = "Categoría del Gasto")]
        public string TipoGasto { get; set; }

        [Required(ErrorMessage = "El concepto del gasto es obligatorio.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        [Display(Name = "Concepto")]
        public string Concepto { get; set; }

        // Escenario 2: monto obligatorio
        [Required(ErrorMessage = "El monto del gasto es obligatorio.")]
        [Range(0.01, 9999999.99, ErrorMessage = "El monto debe ser mayor a cero.")]
        [Display(Name = "Monto (₡)")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        // ================================================================
        // DROPDOWNS
        // ================================================================
        public IEnumerable<SelectListItem> ListaEstados { get; set; }
        public IEnumerable<SelectListItem> ListaTiposGasto { get; set; }
    }
}