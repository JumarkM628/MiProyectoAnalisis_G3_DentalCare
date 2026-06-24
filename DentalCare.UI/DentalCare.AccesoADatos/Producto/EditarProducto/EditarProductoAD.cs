using DentalCare.Abstraccion.AccesoADatos.Producto;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Producto;
using DentalCare.AccesoADatos.Entidades.Bitacora;

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
        public void RegistrarCambioEnBitacora(BitacoraDto bitacora)
        {
            var entidad = new BitacoraEntidad
            {
                Modulo = bitacora.Modulo,
                Accion = bitacora.Accion,
                Descripcion = bitacora.Descripcion,
                NombreUsuario = bitacora.NombreUsuario,
                FechaHora = bitacora.FechaHora
            };

            _contexto.Bitacoras.Add(entidad);
            _contexto.SaveChanges();
        }
    }
}
