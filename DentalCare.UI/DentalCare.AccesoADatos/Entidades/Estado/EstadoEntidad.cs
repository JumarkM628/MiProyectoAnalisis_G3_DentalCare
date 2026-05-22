using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Estado
{
    [Table("FIDE_ESTADO_TB")]
    public class EstadoEntidad
    {
        [Key]
        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
        [Column("NOMBRE_ESTADO")]
        public string NombreEstado { get; set; }
    }
}
