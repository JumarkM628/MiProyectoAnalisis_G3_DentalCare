using DentalCare.Abstraccion.AccesoADatos.Producto;
using DentalCare.Abstraccion.Modelo.Producto;

namespace DentalCare.AccesoADatos.Producto.EditarProducto
{
    public class EditarProductoAD : IEditarProductoAD
    {
        private readonly Contexto _contexto;

        public EditarProductoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public ProductoDto ObtenerProductoPorId(int id)
        {
            var entidad = _contexto.Productos.Find(id);
            if (entidad == null) return null;

            return new ProductoDto
            {
                IdProducto = entidad.ID_PRODUCTO,
                CodigoProducto = entidad.CODIGO_PRODUCTO,
                NombreProducto = entidad.NOMBRE_PRODUCTO,
                CantidadActual = entidad.STOCK_ACTUAL ?? 0
            };
        }

        public bool GuardarCambios(ProductoDto dto)
        {
            var entidad = _contexto.Productos.Find(dto.IdProducto);
            if (entidad == null) return false;

            entidad.CODIGO_PRODUCTO = dto.CodigoProducto;
            entidad.NOMBRE_PRODUCTO = dto.NombreProducto;
            entidad.STOCK_ACTUAL = dto.CantidadActual;

            _contexto.SaveChanges();
            return true;
        }
        // RegistrarCambioEnBitacora eliminado — los triggers SQL
        // en FIDE_EVENTO_TB registran los cambios automáticamente
    }
}