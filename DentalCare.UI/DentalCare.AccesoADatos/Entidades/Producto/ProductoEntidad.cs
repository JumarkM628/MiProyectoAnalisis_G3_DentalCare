using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalCare.AccesoADatos.Entidades.Producto
{
    [Table("FIDE_PRODUCTO_TB")]
    public class ProductoEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_PRODUCTO { get; set; }
        public string CODIGO_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public int ID_CATEGORIA { get; set; }
        public string UNIDAD_MEDIDA { get; set; }
        public int? STOCK_ACTUAL { get; set; }
        public int? STOCK_MINIMO { get; set; }
        public string LOTE { get; set; }
        public DateTime? FECHA_VENCIMIENTO { get; set; }
        public int ID_ESTADO { get; set; }
        public int? REGISTRADO_POR { get; set; }
    }
}
