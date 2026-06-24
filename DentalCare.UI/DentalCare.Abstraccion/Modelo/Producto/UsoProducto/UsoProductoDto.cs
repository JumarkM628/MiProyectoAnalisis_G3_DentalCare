using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Producto.UsoProducto
{
    public class UsoProductoDto
    {
        public int IdUso { get; set; }
        public int IdCita { get; set; }
        public int IdProcedimiento { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un producto.")]
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
    }
}