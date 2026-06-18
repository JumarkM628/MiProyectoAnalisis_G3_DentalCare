using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Citas
{
    [Table("FIDE_MOTIVO_CANCELACION_TB")]
    public class MotivoCancelacionEntidad
    {
        [Key]
        [Column("ID_CANCELACION")]
        public int IdCancelacion { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
