using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Producto;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Producto;

namespace DentaCare.LogicaDeNegocio.Producto.EditarProducto
{
    public class EditarProductoLN : IEditarProductoLN
    {
        private readonly IEditarProductoAD _editarProductoAD;

        public EditarProductoLN(IEditarProductoAD editarProductoAD)
        {
            _editarProductoAD = editarProductoAD;
        }

        public ProductoDto ObtenerProductoPorId(int id)
        {
            return _editarProductoAD.ObtenerProductoPorId(id);
        }

        public string EditarProducto(ProductoDto dto, string nombreUsuario)
        {
            var productoActual = _editarProductoAD.ObtenerProductoPorId(dto.IdProducto);
            if (productoActual == null)
                return "El producto no fue encontrado.";

            int cantidadAnterior = productoActual.CantidadActual;
            bool guardado = _editarProductoAD.GuardarCambios(dto);
            if (!guardado)
                return "Ocurrió un error al guardar los cambios.";


            var bitacora = new BitacoraDto
            {
                Modulo = "Inventario",
                Accion = "Modificación",
                Descripcion = $"Producto '{dto.NombreProducto}' (Código: {dto.CodigoProducto}) actualizado. " +
                                 $"Cantidad anterior: {cantidadAnterior} → Cantidad nueva: {dto.CantidadActual}.",
                NombreUsuario = nombreUsuario,
                FechaHora = DateTime.Now
            };

            _editarProductoAD.RegistrarCambioEnBitacora(bitacora);

            return null; 
        }
    }
}