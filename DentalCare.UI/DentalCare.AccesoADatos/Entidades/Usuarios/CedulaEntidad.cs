using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Usuarios
{
    [Table("FIDE_CEDULA_TB")]
    public class CedulaEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("TIPO_CEDULA")]
        public string TipoCedula { get; set; }

        [Column("NUMERO_CEDULA")]
        public string NumeroCedula { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }

    }
}
