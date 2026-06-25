using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Citas
{
    [Table("FIDE_MOTIVO_CANCELACION_TB")]
    public class MotivoCancelacionEntidad
    {
        [Key]
        [Column("ID_CANCELACION")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int IdCancelacion { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}