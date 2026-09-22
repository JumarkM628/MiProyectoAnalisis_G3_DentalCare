using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Bitacora
{
    [Table("FIDE_EVENTO_TB")]
    public class EventoEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdEvento")]
        public int IdEvento { get; set; }

        [Column("TablaDeEvento")]
        public string TablaDeEvento { get; set; }

        [Column("TipoDeEvento")]
        public string TipoDeEvento { get; set; }

        [Column("FechaDeEvento")]
        public DateTime FechaDeEvento { get; set; }

        [Column("DescripcionDeEvento")]
        public string DescripcionDeEvento { get; set; }

        [Column("StackTrace")]
        public string StackTrace { get; set; }

        [Column("DatosAnteriores")]
        public string DatosAnteriores { get; set; }

        [Column("DatosPosteriores")]
        public string DatosPosteriores { get; set; }
    }
}