using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Inventario
{
    public class ReporteInventarioAD
    {
        private readonly Contexto _contexto;

        public ReporteInventarioAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<ProductoInventarioDto> ObtenerProductosStockBajo(int? categoriaId = null)
        {
            var productQuery = _contexto.Productos
                .Where(prod => (prod.STOCK_ACTUAL ?? 0) <= (prod.STOCK_MINIMO ?? 0));

            if (categoriaId.HasValue)
                productQuery = productQuery.Where(prod => prod.ID_CATEGORIA == categoriaId.Value);
            var providerPerProduct = (from rel in _contexto.ProveedorProductos
                                      join prov in _contexto.Proveedores on rel.IdProveedor equals prov.IdProveedor
                                      select new
                                      {
                                          rel.IdProducto,
                                          ProveedorNombre = (prov.Nombre ?? "") + " " + (prov.PrimerApellido ?? "")
                                      })
                                      .GroupBy(x => x.IdProducto)
                                      .Select(g => new { IdProducto = g.Key, ProveedorNombre = g.Select(x => x.ProveedorNombre).FirstOrDefault() });
            var query = from prod in productQuery
                        join cat in _contexto.CategoriasProducto on prod.ID_CATEGORIA equals cat.IdCategoria into catGrp
                        from cat in catGrp.DefaultIfEmpty()
                        join provInfo in providerPerProduct on prod.ID_PRODUCTO equals provInfo.IdProducto into provGrp
                        from provInfo in provGrp.DefaultIfEmpty()
                        select new ProductoInventarioDto
                        {
                            IdProducto = prod.ID_PRODUCTO,
                            NombreProducto = prod.NOMBRE_PRODUCTO,
                            CategoriaProducto = cat != null ? cat.NombreCategoria : "-",
                            StockActual = prod.STOCK_ACTUAL ?? 0,
                            StockMinimo = prod.STOCK_MINIMO ?? 0,
                            FechaVencimiento = prod.FECHA_VENCIMIENTO,
                            Proveedor = provInfo != null ? provInfo.ProveedorNombre.Trim() : "-",
                            FechaRegistro = prod.FECHA_REGISTRO,       
                            FechaModificacion = prod.FECHA_MODIFICACION 
                        };

            return query
                .OrderBy(p => p.NombreProducto)
                .ToList();
        }
    }
}
