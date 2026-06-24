using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Producto.UsoProducto;

namespace DentalCare.Abstraccion.AccesoADatos.UsoProducto
{
    public interface IRegistrarUsoProductoAD
    {
        int ObtenerOCrearProcedimiento(int idCita);
        List<UsoProductoDto> ObtenerProductosUsadosPorCita(int idCita);
        bool GuardarUso(UsoProductoDto dto, int idProcedimiento);
        bool ExisteUsoRegistrado(int idCita);
    }
}
