using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Usuarios
{
    [Table("FIDE_ESPECIALIDAD_USUARIO_TB")]
    public class EspecialidadEntidad
    {
        [Key]
        [Column("ID_ESPECIALIDAD")]
        public int IdEspecialidad { get; set; }
        [Column("NOMBRE_ESPECIALIDAD")]
        public string NombreEspecialidad { get; set; }
        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
