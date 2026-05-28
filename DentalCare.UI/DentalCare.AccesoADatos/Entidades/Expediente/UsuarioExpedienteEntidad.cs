using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Expediente
{
    [Table("FIDE_USUARIO_EXPEDIENTE_TB")]
    public class UsuarioExpedienteEntidad
    {
        [Key, Column("ID_USUARIO", Order = 0)]
        public int IdUsuario { get; set; }

        [Key, Column("ID_EXPEDIENTE", Order = 1)]
        public int IdExpediente { get; set; }
    }
}
