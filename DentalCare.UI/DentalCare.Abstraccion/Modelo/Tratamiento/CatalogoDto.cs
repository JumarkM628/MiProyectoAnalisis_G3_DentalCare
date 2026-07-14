using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Catalogo
{
    public class CatalogoDto
    {
        // ================================================================
        // SOLO LECTURA (tabla del catálogo)
        // ================================================================
        [Display(Name = "ID")]
        public int IdCatalogo { get; set; }

        [Display(Name = "Tratamiento")]
        public string Nombre { get; set; }

        [Display(Name = "Categoría")]
        public string Categoria { get; set; }

        [Display(Name = "Duración (min)")]
        public int? DuracionMin { get; set; }

        [Display(Name = "Costo Actual")]
        public decimal Costo { get; set; }

        [Display(Name = "Costo Anterior")]
        public decimal? CostoAnterior { get; set; }

        [Display(Name = "Última Actualización")]
        public DateTime? FechaActualizacion { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        // Texto formateado para dropdown en formulario de pago
        public string TextoDropdown => $"{Nombre} (₡{Costo:N0})";

        // ================================================================
        // FORMULARIO (actualizar costo)
        // ================================================================
        [Required(ErrorMessage = "El nuevo costo es obligatorio.")]
        [Range(0.01, 9999999.99, ErrorMessage = "El costo debe ser mayor a cero.")]
        [Display(Name = "Nuevo Costo (₡)")]
        public decimal NuevoCosto { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        [Display(Name = "Motivo del cambio")]
        public string Motivocambio { get; set; }

        // ================================================================
        // DROPDOWNS
        // ================================================================
        public IEnumerable<SelectListItem> ListaEstados { get; set; }
    }
}