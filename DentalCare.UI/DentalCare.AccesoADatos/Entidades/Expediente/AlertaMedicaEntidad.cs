using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Expediente
{
    [Table("FIDE_ALERTA_MEDICA_TB")]
    public class AlertaMedicaEntidad
    {
        [Key]
        [Column("ID_ALERTA")]
        public int IdAlerta { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("NIVEL_RIESGO")]
        public string NivelRiesgo { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
