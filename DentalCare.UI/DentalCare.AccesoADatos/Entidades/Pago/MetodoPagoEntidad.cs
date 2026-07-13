using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Pago
{
    [Table("FIDE_METODO_PAGO_TB")]
    public class MetodoPagoEntidad
    {
        [Key]
        [Column("ID_METODO_PAGO")]
        public int IdMetodoPago { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
