using System.Collections.Generic;
using DentalCare.Abstraccion.AccesoADatos.UsoProducto;
using DentalCare.Abstraccion.LogicaDeNegocio.UsoProducto;
using DentalCare.Abstraccion.Modelo.Producto.UsoProducto;

namespace DentaCare.LogicaDeNegocio.UsoProducto.RegistrarUsoProducto
{
    public class RegistrarUsoProductoLN : IRegistrarUsoProductoLN
    {
        private readonly IRegistrarUsoProductoAD _registrarUsoProductoAD;

        public RegistrarUsoProductoLN(IRegistrarUsoProductoAD registrarUsoProductoAD)
        {
            _registrarUsoProductoAD = registrarUsoProductoAD;
        }
        public List<UsoProductoDto> ObtenerProductosUsadosPorCita(int idCita)
        {
            return _registrarUsoProductoAD.ObtenerProductosUsadosPorCita(idCita);
        }
        public string RegistrarUso(UsoProductoDto dto)
        {
            if (dto.Cantidad <= 0)
                return "La cantidad debe ser mayor a 0.";

            int idProcedimiento = _registrarUsoProductoAD.ObtenerOCrearProcedimiento(dto.IdCita);

            bool guardado = _registrarUsoProductoAD.GuardarUso(dto, idProcedimiento);
            if (!guardado)
                return "Ocurrió un error al registrar el producto.";

            return null;
        }
    }
}
