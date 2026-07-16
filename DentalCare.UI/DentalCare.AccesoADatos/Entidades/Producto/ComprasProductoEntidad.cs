using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Producto
{
    [Table("FIDE_COMPRA_PRODUCTO_TB")]
    public class ComprasProductoEntidad
    {
        [Key]
        public int ID_COMPRA { get; set; }
        public int ID_PRODUCTO { get; set; }
        public int ID_PROVEEDOR { get; set; }
        public int CANTIDAD { get; set; }
        public DateTime FECHA { get; set; }
        public int ID_ESTADO { get; set; }
    }
}