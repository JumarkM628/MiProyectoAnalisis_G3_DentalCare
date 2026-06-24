using DentalCare.Abstraccion.Modelo.Producto;

namespace DentalCare.Abstraccion.AccesoADatos.Producto
{
    public interface IRegistrarProductoAD
    {
        bool ExisteCodigoProducto(string codigo, int idExcluir = 0);
        bool GuardarNuevoProducto(ProductoDto dto);
        bool ActualizarProducto(ProductoDto dto);
    }
}
