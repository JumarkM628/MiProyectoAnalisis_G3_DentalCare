using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Expediente
{
    [Table("FIDE_CONSENTIMIENTO_TB")]
    public class ConsentimientoEntidad
    {
        [Key]
        [Column("ID_CONSENTIMIENTO")]
        public int IdConsentimiento { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("FECHA")]
        public DateTime? Fecha { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
