using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Odontograma
{
    [Table("FIDE_ODONTOGRAMA_DETALLE_TB")]
    public class OdontogramaDetalleEntidad
{
    [Key]
    [Column("ID_DETALLE")]
    public int IdDetalle { get; set; }

    [Column("ID_ODONTOGRAMA")]
    public int IdOdontograma { get; set; }

    [Column("ID_PIEZA")]
    public int IdPieza { get; set; }

    [Column("ESTADO_PIEZA")]
    public string EstadoPieza { get; set; }
}
}