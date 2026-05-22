using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Usuarios
{
    [Table("FIDE_TELEFONO_TB")]
    public class TelefonoEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }
        [Column("NUMERO_TELEFONO")]
        public string Telefono { get; set; }
        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
