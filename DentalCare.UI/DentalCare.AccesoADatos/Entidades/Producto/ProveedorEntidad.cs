using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Producto
{
    [Table("FIDE_PROVEEDOR_TB")]
    public class ProveedorEntidad
    {
        [Key]
        [Column("ID_PROVEEDOR")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProveedor { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("PRIMER_APELLIDO")]
        public string PrimerApellido { get; set; }

        [Column("SEGUNDO_APELLIDO")]
        public string SegundoApellido { get; set; }

        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
    }
}
