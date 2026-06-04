using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Odontograma
{
    [Table("FIDE_ODONTOGRAMA_TB")]
    public class OdontogramaEntidad
    {
        [Key]
        [Column("ID_ODONTOGRAMA")]
        public int IdOdontograma { get; set; }

        [Column("FECHA")]
        public DateTime? Fecha { get; set; }

        [Column("ID_PIEZA")]
        public int IdPieza { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}