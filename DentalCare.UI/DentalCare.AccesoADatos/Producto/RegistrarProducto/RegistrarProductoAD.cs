using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Producto;
using DentalCare.Abstraccion.Modelo.Producto;
using DentalCare.AccesoADatos.Entidades.Producto;

namespace DentalCare.AccesoADatos.Producto.RegistrarProducto
{
    public class RegistrarProductoAD : IRegistrarProductoAD
    {
        private readonly Contexto _contexto;
        public RegistrarProductoAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public bool ExisteCodigoProducto(string codigo, int idExcluir = 0)
        {
            return _contexto.Productos.Any(p =>
                p.CODIGO_PRODUCTO == codigo && p.ID_PRODUCTO != idExcluir);
        }
        public bool GuardarNuevoProducto(ProductoDto dto)
        {
            // Generar el siguiente ID_PRODUCTO
            int nuevoId = _contexto.Productos.Any() 
                ? _contexto.Productos.Max(p => p.ID_PRODUCTO) + 1 
                : 1;

            var entidad = new ProductoEntidad
            {
                ID_PRODUCTO = nuevoId,
                CODIGO_PRODUCTO = dto.CodigoProducto,
                NOMBRE_PRODUCTO = dto.NombreProducto,
                DESCRIPCION = dto.Descripcion,
                ID_CATEGORIA = dto.IdCategoria,
                UNIDAD_MEDIDA = dto.UnidadMedida,
                STOCK_ACTUAL = dto.CantidadActual,
                STOCK_MINIMO = dto.CantidadMinima,
                LOTE = dto.Lote,
                FECHA_VENCIMIENTO = dto.FechaVencimiento,
                ID_ESTADO = 1

                // REGISTRADO_POR = dto.RegistradoPor
            };
            _contexto.Productos.Add(entidad);
            System.Diagnostics.Debug.WriteLine("IdCategoria recibido: " + dto.IdCategoria);
            _contexto.SaveChanges();
            _contexto.ProveedorProductos.Add(new ProveedorProductoEntidad
            {
                IdProveedor = dto.IdProveedor,
                IdProducto = entidad.ID_PRODUCTO
            });
            _contexto.SaveChanges();
            return true;
        }
        public bool ActualizarProducto(ProductoDto dto)
        {
            var entidad = _contexto.Productos.Find(dto.IdProducto);
            if (entidad == null) return false;
            entidad.CODIGO_PRODUCTO = dto.CodigoProducto;
            entidad.NOMBRE_PRODUCTO = dto.NombreProducto;
            entidad.DESCRIPCION = dto.Descripcion;
            entidad.ID_CATEGORIA = dto.IdCategoria;
            entidad.UNIDAD_MEDIDA = dto.UnidadMedida;
            entidad.STOCK_ACTUAL = dto.CantidadActual;
            entidad.STOCK_MINIMO = dto.CantidadMinima;
            entidad.LOTE = dto.Lote;
            entidad.FECHA_VENCIMIENTO = dto.FechaVencimiento;
            _contexto.SaveChanges();
            return true;
        }
    }
}
