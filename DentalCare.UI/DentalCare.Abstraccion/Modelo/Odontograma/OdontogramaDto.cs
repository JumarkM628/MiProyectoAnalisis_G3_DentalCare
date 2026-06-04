using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Odontograma
{
    public class OdontogramaDto
    {
        public int IdOdontograma { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        public int IdExpediente { get; set; }

        // Detalles de piezas seleccionadas
        public List<OdontogramaDetalleDto> Detalles { get; set; } = new List<OdontogramaDetalleDto>();

        // Para el dropdown de piezas
        public IEnumerable<SelectListItem> ListaPiezas { get; set; }
    }

    public class OdontogramaDetalleDto
    {
        public int IdDetalle { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una pieza dental.")]
        [Display(Name = "Pieza Dental")]
        public int IdPieza { get; set; }

        public string NumeroPieza { get; set; }

        [Required(ErrorMessage = "Debe indicar el estado de la pieza.")]
        [Display(Name = "Estado de la Pieza")]
        public string EstadoPieza { get; set; }
    }
}