using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Tratamiento
{
    [Table("FIDE_TRATAMIENTO_TB")]
    public class PlanTratamientoEntidad
    {
        [Key]
        public int ID_TRATAMIENTO { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime? FECHA_INICIO { get; set; }
        public DateTime? FECHA_FIN { get; set; }
        public int ID_ESTADO { get; set; }
    }
}
