using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Producto
{
    [Table("FIDE_USO_PRODUCTO_TB")]
    public class UsoProductoEntidad
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //added maybe delete later
        public int ID_USO { get; set; }
        public int ID_PRODUCTO { get; set; }
        public int ID_PROCEDIMIENTO { get; set; }
        public int? CANTIDAD { get; set; }
        public int ID_ESTADO { get; set; }
    }
}
