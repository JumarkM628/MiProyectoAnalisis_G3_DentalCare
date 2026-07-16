using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Gastos
{
    [Table("FIDE_GASTO_TB")]
    public class GastoEntidad
    {
        [Key]
        [Column("ID_GASTO")]
        public int IdGasto { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("MONTO")]
        public decimal? Monto { get; set; }

        [Column("FECHA")]
        public DateTime? Fecha { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}