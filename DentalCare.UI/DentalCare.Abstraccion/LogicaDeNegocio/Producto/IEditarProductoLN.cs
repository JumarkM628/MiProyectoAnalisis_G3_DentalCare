using DentalCare.Abstraccion.Modelo.Producto;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Producto
{
    public interface IEditarProductoLN
    {
        ProductoDto ObtenerProductoPorId(int id);
        string EditarProducto(ProductoDto dto, string nombreUsuario);
    }
}
