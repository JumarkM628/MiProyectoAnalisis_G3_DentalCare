using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Producto.UsoProducto;

namespace DentalCare.Abstraccion.LogicaDeNegocio.UsoProducto
{
    public interface IRegistrarUsoProductoLN
    {
        List<UsoProductoDto> ObtenerProductosUsadosPorCita(int idCita);
        string RegistrarUso(UsoProductoDto dto);
    }
}