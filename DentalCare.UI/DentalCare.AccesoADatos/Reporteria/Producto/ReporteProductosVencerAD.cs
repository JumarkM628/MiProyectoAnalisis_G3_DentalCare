using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Producto
{
    public class ReporteProductosVencerAD : IReporteProductosVencerAD
    {
        private readonly Contexto _contexto;

        public ReporteProductosVencerAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<ReporteProductosVencerDto> ObtenerProductosPorVencer(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var consulta = _contexto.Productos
                .Where(p => p.ID_ESTADO == 1 &&
                            p.FECHA_VENCIMIENTO.HasValue);

            if (fechaInicio.HasValue)
                consulta = consulta.Where(p => p.FECHA_VENCIMIENTO >= fechaInicio.Value);

            if (fechaFin.HasValue)
                consulta = consulta.Where(p => p.FECHA_VENCIMIENTO <= fechaFin.Value);

            return consulta
                .Select(p => new ReporteProductosVencerDto
                {
                    IdProducto = p.ID_PRODUCTO,
                    CodigoProducto = p.CODIGO_PRODUCTO,
                    NombreProducto = p.NOMBRE_PRODUCTO,
                    StockActual = p.STOCK_ACTUAL,
                    FechaVencimiento = p.FECHA_VENCIMIENTO,
                })
                .ToList();
        }
    }
}