using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Producto
{
    [Table("FIDE_CATEGORIA_PRODUCTO_TB")]
    public class CategoriaProductoEntidad
    {
        [Key]
        [Column("ID_CATEGORIA")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCategoria { get; set; }

        [Column("NOMBRE_CATEGORIA")]
        public string NombreCategoria { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}

