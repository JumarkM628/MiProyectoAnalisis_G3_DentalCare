using DentalCare.Abstraccion.AccesoADatos.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Producto;
using DentalCare.Abstraccion.Modelo.Producto;
using System;

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
            if (dto.CantidadActual < 0)
                return "La cantidad actual no puede ser negativa.";
            if (dto.CantidadMinima < 0)
                return "La cantidad mínima no puede ser negativa.";
            if (dto.FechaVencimiento.Date < DateTime.Today)
                return "La fecha de vencimiento no puede ser anterior a la fecha actual.";

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