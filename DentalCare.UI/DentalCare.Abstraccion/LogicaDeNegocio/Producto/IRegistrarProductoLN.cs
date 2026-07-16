using DentalCare.Abstraccion.Modelo.Producto;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Producto
{
    public interface IRegistrarProductoLN
    {
        string RegistrarProducto(ProductoDto dto);
        string ActualizarProducto(ProductoDto dto);
    }
}
