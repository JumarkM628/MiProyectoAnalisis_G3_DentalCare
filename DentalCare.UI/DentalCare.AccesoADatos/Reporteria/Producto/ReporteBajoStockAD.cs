using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Producto
{
    public class ReporteBajoStockAD : IReporteBajoStockAD
    {
        private readonly Contexto _contexto;

        public ReporteBajoStockAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<ReporteBajoStockDto> ObtenerProductosBajoStock()
        {
            var consulta =
                from producto in _contexto.Productos

                join categoria in _contexto.CategoriasProducto
                    on producto.ID_CATEGORIA equals categoria.IdCategoria

                join proveedorProducto in _contexto.ProveedorProductos
                    on producto.ID_PRODUCTO equals proveedorProducto.IdProducto into proveedorJoin

                from proveedorProducto in proveedorJoin.DefaultIfEmpty()

                join proveedor in _contexto.Proveedores
                    on proveedorProducto.IdProveedor equals proveedor.IdProveedor into proveedorFinal

                from proveedor in proveedorFinal.DefaultIfEmpty()

                where producto.ID_ESTADO == 1 &&
                      producto.STOCK_ACTUAL <= producto.STOCK_MINIMO

                select new ReporteBajoStockDto
                {
                    IdProducto = producto.ID_PRODUCTO,
                    CodigoProducto = producto.CODIGO_PRODUCTO,
                    NombreProducto = producto.NOMBRE_PRODUCTO,
                    CategoriaProducto = categoria.NombreCategoria,
                    StockActual = producto.STOCK_ACTUAL,
                    StockMinimo = producto.STOCK_MINIMO,
                    FechaVencimiento = producto.FECHA_VENCIMIENTO,
                    Proveedor = proveedor == null
                        ? string.Empty
                        : proveedor.Nombre + " " +
                          proveedor.PrimerApellido + " " +
                          proveedor.SegundoApellido
                };

            return consulta.ToList();
        }

        public List<ReporteBajoStockDto> ObtenerProductosBajoStockPorCategoria(int idCategoria)
        {
            var consulta =
                from producto in _contexto.Productos

                join categoria in _contexto.CategoriasProducto
                    on producto.ID_CATEGORIA equals categoria.IdCategoria

                join proveedorProducto in _contexto.ProveedorProductos
                    on producto.ID_PRODUCTO equals proveedorProducto.IdProducto into proveedorJoin

                from proveedorProducto in proveedorJoin.DefaultIfEmpty()

                join proveedor in _contexto.Proveedores
                    on proveedorProducto.IdProveedor equals proveedor.IdProveedor into proveedorFinal

                from proveedor in proveedorFinal.DefaultIfEmpty()

                where producto.ID_ESTADO == 1 &&
                      producto.STOCK_ACTUAL <= producto.STOCK_MINIMO

                select new ReporteBajoStockDto
                {
                    IdProducto = producto.ID_PRODUCTO,
                    CodigoProducto = producto.CODIGO_PRODUCTO,
                    NombreProducto = producto.NOMBRE_PRODUCTO,
                    CategoriaProducto = categoria.NombreCategoria,
                    StockActual = producto.STOCK_ACTUAL,
                    StockMinimo = producto.STOCK_MINIMO,
                    FechaVencimiento = producto.FECHA_VENCIMIENTO,
                    Proveedor = proveedor == null
                        ? string.Empty
                        : proveedor.Nombre + " " +
                          proveedor.PrimerApellido + " " +
                          proveedor.SegundoApellido
                };

            return consulta.ToList();
        }
    }
}

