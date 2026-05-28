using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Expedientes
{
    [Table("FIDE_EXPEDIENTE_TB")]
    public class ExpedienteEntidad
    {
        [Key]
        [Column("ID_EXPEDIENTE")]
        public int IdExpediente { get; set; }

        [Column("FECHA_DE_CREACION")]
        public DateTime? FechaDeCreacion { get; set; }

        [Column("ID_PROCEDIMIENTO")]
        public int IdProcedimiento { get; set; }

        [Column("ID_NOTA")]
        public int IdNota { get; set; }

        [Column("ID_ARCHIVO")]
        public int IdArchivo { get; set; }

        [Column("ID_ALERTA")]
        public int IdAlerta { get; set; }

        [Column("ID_CONSENTIMIENTO")]
        public int IdConsentimiento { get; set; }

        [Column("ID_ODONTOGRAMA")]
        public int IdOdontograma { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }

}
