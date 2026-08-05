using DentalCare.Abstraccion.AccesoADatos.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Producto;
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

            bool guardado = _editarProductoAD.GuardarCambios(dto);
            if (!guardado)
                return "Ocurrió un error al guardar los cambios.";

            // El trigger trg_Producto_Update registra el cambio automáticamente
            return null;
        }
    }
}