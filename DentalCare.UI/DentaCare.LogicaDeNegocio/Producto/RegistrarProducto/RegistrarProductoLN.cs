using DentalCare.Abstraccion.AccesoADatos.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Producto;
using DentalCare.Abstraccion.Modelo.Producto;

namespace DentaCare.LogicaDeNegocio.Producto.RegistrarProducto
{
    public class RegistrarProductoLN : IRegistrarProductoLN
    {
        private readonly IRegistrarProductoAD _registrarProductoAD;

        public RegistrarProductoLN(IRegistrarProductoAD registrarProductoAD)
        {
            _registrarProductoAD = registrarProductoAD;
        }

        public string RegistrarProducto(ProductoDto dto)
        {
            if (_registrarProductoAD.ExisteCodigoProducto(dto.CodigoProducto))
                return "Ya existe un producto registrado con ese código.";
            bool guardado = _registrarProductoAD.GuardarNuevoProducto(dto);
            if (!guardado)
                return "Ocurrió un error al registrar el producto.";

            return null;
        }

        public string ActualizarProducto(ProductoDto dto)
        {
            if (_registrarProductoAD.ExisteCodigoProducto(dto.CodigoProducto, dto.IdProducto))
                return "Ya existe otro producto registrado con ese código.";
            bool actualizado = _registrarProductoAD.ActualizarProducto(dto);
            if (!actualizado)
                return "Ocurrió un error al actualizar el producto.";

            return null;
        }
    }
}