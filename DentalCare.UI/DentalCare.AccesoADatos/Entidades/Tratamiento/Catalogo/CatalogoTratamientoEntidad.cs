using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Catalogo
{
    [Table("FIDE_CATALOGO_TRATAMIENTO_TB")]
    public class CatalogoTratamientoEntidad
    {
        [Key]
        [Column("ID_CATALOGO")]
        public int IdCatalogo { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("CATEGORIA")]
        public string Categoria { get; set; }

        [Column("DURACION_MIN")]
        public int? DuracionMin { get; set; }

        [Column("COSTO")]
        public decimal Costo { get; set; }

        [Column("COSTO_ANTERIOR")]
        public decimal? CostoAnterior { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }

        [Column("FECHA_ACTUALIZACION")]
        public DateTime? FechaActualizacion { get; set; }
    }
}