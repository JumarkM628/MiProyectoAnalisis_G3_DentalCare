using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Tratamientos
{
    [Table("FIDE_TRATAMIENTO_TB")]
    public class PlanTratamientoEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Added maybe delete later
        [Column("ID_TRATAMIENTO")]
        public int IdTratamiento { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("FECHA_INICIO")]
        public DateTime? FechaInicio { get; set; }

        [Column("FECHA_FIN")]
        public DateTime? FechaFin { get; set; }

        [Column("MONTO")]
        public decimal? Monto { get; set; }

        [Column("ID_CITA")]
        public int? IdCita { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}