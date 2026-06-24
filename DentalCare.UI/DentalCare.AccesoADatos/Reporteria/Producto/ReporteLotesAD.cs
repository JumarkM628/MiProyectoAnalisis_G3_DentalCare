using System.Collections.Generic;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Producto
{
    public class ReporteLotesAD : IReporteLotesAD
    {
        private readonly Contexto _contexto;

        public ReporteLotesAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public List<ProductoLoteFrecuenciaDto> ObtenerLotesMasUtilizados()
        {
            return (from uso in _contexto.UsoProductos
                    join prod in _contexto.Productos on uso.ID_PRODUCTO equals prod.ID_PRODUCTO
                    group uso by new { prod.ID_PRODUCTO, prod.CODIGO_PRODUCTO, prod.NOMBRE_PRODUCTO, prod.LOTE }
                    into g
                    orderby g.Sum(x => x.CANTIDAD ?? 0) descending
                    select new ProductoLoteFrecuenciaDto
                    {
                        IdProducto = g.Key.ID_PRODUCTO,
                        CodigoProducto = g.Key.CODIGO_PRODUCTO,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        Lote = g.Key.LOTE,
                        CantidadTotal = g.Sum(x => x.CANTIDAD ?? 0)
                    }).ToList();
        }
        public List<ProductoLoteFrecuenciaDto> ObtenerLotesMenosUtilizados()
        {
            return (from uso in _contexto.UsoProductos
                    join prod in _contexto.Productos on uso.ID_PRODUCTO equals prod.ID_PRODUCTO
                    group uso by new { prod.ID_PRODUCTO, prod.CODIGO_PRODUCTO, prod.NOMBRE_PRODUCTO, prod.LOTE }
                    into g
                    orderby g.Sum(x => x.CANTIDAD ?? 0) ascending
                    select new ProductoLoteFrecuenciaDto
                    {
                        IdProducto = g.Key.ID_PRODUCTO,
                        CodigoProducto = g.Key.CODIGO_PRODUCTO,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        Lote = g.Key.LOTE,
                        CantidadTotal = g.Sum(x => x.CANTIDAD ?? 0)
                    }).ToList();
        }
        public List<ProductoLoteFrecuenciaDto> ObtenerLotesMasComprados()
        {
            return (from compra in _contexto.ComprasProducto
                    join prod in _contexto.Productos on compra.ID_PRODUCTO equals prod.ID_PRODUCTO
                    group compra by new { prod.ID_PRODUCTO, prod.CODIGO_PRODUCTO, prod.NOMBRE_PRODUCTO, prod.LOTE }
                    into g
                    orderby g.Sum(x => x.CANTIDAD) descending
                    select new ProductoLoteFrecuenciaDto
                    {
                        IdProducto = g.Key.ID_PRODUCTO,
                        CodigoProducto = g.Key.CODIGO_PRODUCTO,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        Lote = g.Key.LOTE,
                        CantidadTotal = g.Sum(x => x.CANTIDAD)
                    }).ToList();
        }
        public List<ProductoLoteFrecuenciaDto> ObtenerLotesMenosComprados()
        {
            return (from compra in _contexto.ComprasProducto
                    join prod in _contexto.Productos on compra.ID_PRODUCTO equals prod.ID_PRODUCTO
                    group compra by new { prod.ID_PRODUCTO, prod.CODIGO_PRODUCTO, prod.NOMBRE_PRODUCTO, prod.LOTE }
                    into g
                    orderby g.Sum(x => x.CANTIDAD) ascending
                    select new ProductoLoteFrecuenciaDto
                    {
                        IdProducto = g.Key.ID_PRODUCTO,
                        CodigoProducto = g.Key.CODIGO_PRODUCTO,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        Lote = g.Key.LOTE,
                        CantidadTotal = g.Sum(x => x.CANTIDAD)
                    }).ToList();
        }
        public List<HistorialLoteTratamientoDto> ObtenerHistorialLotePorTratamiento()
        {
            return (from uso in _contexto.UsoProductos
                    join prod in _contexto.Productos on uso.ID_PRODUCTO equals prod.ID_PRODUCTO
                    join proc in _contexto.Procedimientos on uso.ID_PROCEDIMIENTO equals proc.ID_PROCEDIMIENTO
                    join trat in _contexto.PlanesTratamiento on proc.ID_TRATAMIENTO equals trat.ID_TRATAMIENTO
                    group new { uso, trat } by new
                    {
                        trat.DESCRIPCION,
                        prod.NOMBRE_PRODUCTO,
                        prod.LOTE
                    }
                    into g
                    select new HistorialLoteTratamientoDto
                    {
                        NombreTratamiento = g.Key.DESCRIPCION,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        Lote = g.Key.LOTE,
                        CantidadTotal = g.Sum(x => x.uso.CANTIDAD ?? 0)
                    }).ToList();
        }
    }
}
