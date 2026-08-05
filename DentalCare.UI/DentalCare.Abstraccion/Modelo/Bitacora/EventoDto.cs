using System;
using System.ComponentModel.DataAnnotations;

namespace DentalCare.Abstraccion.Modelo.Bitacora
{
    public class EventoDto
    {
        [Display(Name = "ID")]
        public int IdEvento { get; set; }

        [Display(Name = "Tabla")]
        public string TablaDeEvento { get; set; }

        [Display(Name = "Tipo")]
        public string TipoDeEvento { get; set; }

        [Display(Name = "Fecha")]
        public DateTime FechaDeEvento { get; set; }

        [Display(Name = "Descripción")]
        public string DescripcionDeEvento { get; set; }

        [Display(Name = "Stack Trace")]
        public string StackTrace { get; set; }

        [Display(Name = "Datos Anteriores")]
        public string DatosAnteriores { get; set; }

        [Display(Name = "Datos Posteriores")]
        public string DatosPosteriores { get; set; }
    }
}