using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Pago
{
    [Table("FIDE_ANTICIPO_TB")]
    public class AnticipoEntidad
    {
        [Key]
        [Column("ID_ANTICIPO")]
        public int IdAnticipo { get; set; }

        [Column("ID_METODO_PAGO")]
        public int IdMetodoPago { get; set; }

        [Column("FECHA")]
        public DateTime? Fecha { get; set; }

        [Column("MONTO")]
        public decimal? Monto { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
