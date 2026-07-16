using System.Collections.Generic;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Producto
{
    public class ReporteProductosAD : IReporteProductosAD
    {
        private readonly Contexto _contexto;

        public ReporteProductosAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public List<ProductoFrecuenciaDto> ObtenerMasUtilizados()
        {
            return (from uso in _contexto.UsoProductos
                    join prod in _contexto.Productos on uso.ID_PRODUCTO equals prod.ID_PRODUCTO
                    group uso by new { prod.ID_PRODUCTO, prod.CODIGO_PRODUCTO, prod.NOMBRE_PRODUCTO }
                    into g
                    orderby g.Sum(x => x.CANTIDAD ?? 0) descending
                    select new ProductoFrecuenciaDto
                    {
                        IdProducto = g.Key.ID_PRODUCTO,
                        CodigoProducto = g.Key.CODIGO_PRODUCTO,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        CantidadTotal = g.Sum(x => x.CANTIDAD ?? 0)
                    }).ToList();
        }
        public List<ProductoFrecuenciaDto> ObtenerMenosUtilizados()
        {
            return (from uso in _contexto.UsoProductos
                    join prod in _contexto.Productos on uso.ID_PRODUCTO equals prod.ID_PRODUCTO
                    group uso by new { prod.ID_PRODUCTO, prod.CODIGO_PRODUCTO, prod.NOMBRE_PRODUCTO }
                    into g
                    orderby g.Sum(x => x.CANTIDAD ?? 0) ascending
                    select new ProductoFrecuenciaDto
                    {
                        IdProducto = g.Key.ID_PRODUCTO,
                        CodigoProducto = g.Key.CODIGO_PRODUCTO,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        CantidadTotal = g.Sum(x => x.CANTIDAD ?? 0)
                    }).ToList();
        }
        public List<ProductoFrecuenciaDto> ObtenerMasComprados()
        {
            return (from compra in _contexto.ComprasProducto
                    join prod in _contexto.Productos on compra.ID_PRODUCTO equals prod.ID_PRODUCTO
                    group compra by new { prod.ID_PRODUCTO, prod.CODIGO_PRODUCTO, prod.NOMBRE_PRODUCTO }
                    into g
                    orderby g.Sum(x => x.CANTIDAD) descending
                    select new ProductoFrecuenciaDto
                    {
                        IdProducto = g.Key.ID_PRODUCTO,
                        CodigoProducto = g.Key.CODIGO_PRODUCTO,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        CantidadTotal = g.Sum(x => x.CANTIDAD)
                    }).ToList();
        }
        public List<ProductoFrecuenciaDto> ObtenerMenosComprados()
        {
            return (from compra in _contexto.ComprasProducto
                    join prod in _contexto.Productos on compra.ID_PRODUCTO equals prod.ID_PRODUCTO
                    group compra by new { prod.ID_PRODUCTO, prod.CODIGO_PRODUCTO, prod.NOMBRE_PRODUCTO }
                    into g
                    orderby g.Sum(x => x.CANTIDAD) ascending
                    select new ProductoFrecuenciaDto
                    {
                        IdProducto = g.Key.ID_PRODUCTO,
                        CodigoProducto = g.Key.CODIGO_PRODUCTO,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        CantidadTotal = g.Sum(x => x.CANTIDAD)
                    }).ToList();
        }
        public List<HistorialTratamientoDto> ObtenerHistorialPorTratamiento()
        {
            return (from uso in _contexto.UsoProductos
                    join prod in _contexto.Productos on uso.ID_PRODUCTO equals prod.ID_PRODUCTO
                    join proc in _contexto.Procedimientos on uso.ID_PROCEDIMIENTO equals proc.ID_PROCEDIMIENTO
                    join trat in _contexto.PlanesTratamiento on proc.ID_TRATAMIENTO equals trat.IdTratamiento
                    join usrCita in _contexto.UsuarioCitas on proc.ID_CITA equals usrCita.IdCita
                    join usr in _contexto.Usuarios on usrCita.IdUsuario equals usr.IdUsuario
                    group new { uso, trat, usr } by new
                    {
                        trat.Descripcion,
                        prod.NOMBRE_PRODUCTO,
                        NombreDoctora = usr.Nombre + " " + usr.PrimerApellido
                    }
                    into g
                    select new HistorialTratamientoDto
                    {
                        NombreTratamiento = g.Key.Descripcion,
                        NombreProducto = g.Key.NOMBRE_PRODUCTO,
                        NombreDoctora = g.Key.NombreDoctora,
                        CantidadTotal = g.Sum(x => x.uso.CANTIDAD ?? 0)
                    }).ToList();
        }
    }
    }
