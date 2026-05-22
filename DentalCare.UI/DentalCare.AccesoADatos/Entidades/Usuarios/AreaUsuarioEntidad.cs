using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Usuarios
{
    [Table("FIDE_AREA_USUARIO_TB")]
    public class AreaUsuarioEntidad
    {
        [Key]
        [Column("ID_AREA_USUARIO")]
        public int IdAreaUsuario { get; set; }
        [Column("NOMBRE_TIPO_USUARIO")]
        public string NombreTipoUsuario { get; set; }
        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
