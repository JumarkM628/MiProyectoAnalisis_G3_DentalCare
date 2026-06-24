using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Procedimiento
{
    [Table("FIDE_PROCEDIMIENTO_TB")]
    public class ProcedimientoEntidad
    {
        [Key]
        public int ID_PROCEDIMIENTO { get; set; }
        public int ID_CITA { get; set; }
        public int ID_TRATAMIENTO { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime? FECHA { get; set; }
        public string OBSERVACIONES { get; set; }
        public int ID_ESTADO { get; set; }
    }
}