using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Pago
{
    [Table("FIDE_CONTABILIDAD_CITA_TB")]
    public class ContabilidadCitaEntidad
    {
        [Key, Column("ID_CONTABILIDAD", Order = 0)]
        public int IdContabilidad { get; set; }

        [Key, Column("ID_CITA", Order = 1)]
        public int IdCita { get; set; }
    }
}
