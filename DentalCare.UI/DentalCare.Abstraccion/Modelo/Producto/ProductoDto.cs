using System;
using System.ComponentModel.DataAnnotations;

namespace DentalCare.Abstraccion.Modelo.Producto
{
    public class ProductoDto
        {
            public int IdProducto { get; set; }

            [Required(ErrorMessage = "El código del producto es obligatorio.")]
            public string CodigoProducto { get; set; }

            [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
            public string NombreProducto { get; set; }

            [Required(ErrorMessage = "La descripción es obligatoria.")]
            public string Descripcion { get; set; }

            [Required(ErrorMessage = "La categoría es obligatoria.")]
            public int IdCategoria { get; set; }
            public string NombreCategoria { get; set; }  

            [Required(ErrorMessage = "La unidad de medida es obligatoria.")]
            public string UnidadMedida { get; set; }

            [Required(ErrorMessage = "La cantidad actual es obligatoria.")]
            [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa.")]
            public int CantidadActual { get; set; }

            [Required(ErrorMessage = "La cantidad mínima es obligatoria.")]
            [Range(0, int.MaxValue, ErrorMessage = "La cantidad mínima no puede ser negativa.")]
            public int CantidadMinima { get; set; }

            [Required(ErrorMessage = "El lote es obligatorio.")]
            public string Lote { get; set; }

            [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
            [DataType(DataType.Date)]
            public DateTime FechaVencimiento { get; set; }

            [Required(ErrorMessage = "El proveedor es obligatorio.")]
            public int IdProveedor { get; set; }
            public string NombreProveedor { get; set; }  

            public string EstadoProducto { get; set; }
            public int RegistradoPor { get; set; }
        }
    }
