using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Citas
{
    [Table("FIDE_USUARIO_CITA_TB")]
    public class UsuarioCitaEntidad
    {
        [Key, Column("ID_USUARIO", Order = 0)]
        public int IdUsuario { get; set; }

        [Key, Column("ID_CITA", Order = 1)]
        public int IdCita { get; set; }
    }
}
