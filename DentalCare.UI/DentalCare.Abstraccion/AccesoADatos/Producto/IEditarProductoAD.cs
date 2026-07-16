using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Producto;

namespace DentalCare.Abstraccion.AccesoADatos.Producto
{
    public interface IEditarProductoAD
    {
        ProductoDto ObtenerProductoPorId(int id);
        bool GuardarCambios(ProductoDto dto);
        void RegistrarCambioEnBitacora(BitacoraDto bitacora);
    }
}