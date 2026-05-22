using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Usuarios
{
    [Table("FIDE_CORREO_TB")]
    public class CorreoEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("CORREO")]
        public string Correo { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
