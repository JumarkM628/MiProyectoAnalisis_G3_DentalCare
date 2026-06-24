using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Citas
{
    [Table("FIDE_CITAS_TB")]
    public class CitaEntidad
    {
        [Key]
        [Column("ID_CITA")]
        public int IdCita { get; set; }

        [Column("FECHA")]
        public DateTime? Fecha { get; set; }

        [Column("HORA")]
        public TimeSpan? Hora { get; set; }

        [Column("ID_MOTIVO")]
        public int IdMotivo { get; set; }

        [Column("ID_CANCELACION")]
        public int IdCancelacion { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }

        [Column("ID_DOCTOR")]
        public int? IdDoctor { get; set; }
    }
}
