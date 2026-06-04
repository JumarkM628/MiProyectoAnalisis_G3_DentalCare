using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Odontograma
{
    [Table("FIDE_PIEZA_DENTAL_TB")]
    public class PiezaDentalEntidad
    {
        [Key]
        [Column("ID_PIEZA")]
        public int IdPieza { get; set; }

        [Column("NUMERO_PIEZA")]
        public string NumeroPieza { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}