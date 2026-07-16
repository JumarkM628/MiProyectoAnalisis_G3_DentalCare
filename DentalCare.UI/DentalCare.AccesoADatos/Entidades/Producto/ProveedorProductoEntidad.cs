using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Producto
{
    [Table("FIDE_PROVEEDOR_PRODUCTO_TB")]
    public class ProveedorProductoEntidad
    {
        [Key, Column("ID_PROVEEDOR", Order = 0)]
        public int IdProveedor { get; set; }

        [Key, Column("ID_PRODUCTO", Order = 1)]
        public int IdProducto { get; set; }
    }
}
