using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Pago
{
    [Table("FIDE_CONTABILIDAD_TB")]
    public class ContabilidadEntidad
    {
        [Key]
        [Column("ID_CONTABILIDAD")]
        public int IdContabilidad { get; set; }

        [Column("MONTO")]
        public decimal? Monto { get; set; }

        [Column("ID_METODO_PAGO")]
        public int IdMetodoPago { get; set; }

        [Column("ID_GASTO")]
        public int IdGasto { get; set; }

        [Column("ID_ANTICIPO")]
        public int IdAnticipo { get; set; }

        [Column("FECHA")]
        public DateTime? Fecha { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
