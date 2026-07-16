using System.Collections.Generic;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.UsoProducto;
using DentalCare.Abstraccion.Modelo.Producto.UsoProducto;
using DentalCare.AccesoADatos.Entidades.Procedimiento;
using DentalCare.AccesoADatos.Entidades.Producto;

namespace DentalCare.AccesoADatos.UsoProducto.RegistrarUsoProducto
{
    public class RegistrarUsoProductoAD : IRegistrarUsoProductoAD
    {
        private readonly Contexto _contexto;

        public RegistrarUsoProductoAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public int ObtenerOCrearProcedimiento(int idCita)
        {
            var procedimiento = _contexto.Procedimientos
                .FirstOrDefault(p => p.ID_CITA == idCita);

            if (procedimiento != null)
                return procedimiento.ID_PROCEDIMIENTO;

            var nuevo = new ProcedimientoEntidad
            {
                ID_CITA = idCita,
                ID_TRATAMIENTO = 0,
                DESCRIPCION = "Procedimiento generado automáticamente",
                FECHA = System.DateTime.Now,
                ID_ESTADO = 1
            };

            _contexto.Procedimientos.Add(nuevo);
            _contexto.SaveChanges();

            return nuevo.ID_PROCEDIMIENTO;
        }

        public List<UsoProductoDto> ObtenerProductosUsadosPorCita(int idCita)
        {
            return (from uso in _contexto.UsoProductos
                    join proc in _contexto.Procedimientos on uso.ID_PROCEDIMIENTO equals proc.ID_PROCEDIMIENTO
                    join prod in _contexto.Productos on uso.ID_PRODUCTO equals prod.ID_PRODUCTO
                    where proc.ID_CITA == idCita
                    select new UsoProductoDto
                    {
                        IdUso = uso.ID_USO,
                        IdCita = idCita,
                        IdProcedimiento = proc.ID_PROCEDIMIENTO,
                        IdProducto = prod.ID_PRODUCTO,
                        NombreProducto = prod.NOMBRE_PRODUCTO,
                        Cantidad = uso.CANTIDAD ?? 0
                    }).ToList();
        }
        public bool GuardarUso(UsoProductoDto dto, int idProcedimiento)
        {
            var entidad = new UsoProductoEntidad
            {
                ID_PRODUCTO = dto.IdProducto,
                ID_PROCEDIMIENTO = idProcedimiento,
                CANTIDAD = dto.Cantidad,
                ID_ESTADO = 1
            };

            _contexto.UsoProductos.Add(entidad);
            _contexto.SaveChanges();
            return true;
        }
        public bool ExisteUsoRegistrado(int idCita)
        {
            return (from uso in _contexto.UsoProductos
                    join proc in _contexto.Procedimientos on uso.ID_PROCEDIMIENTO equals proc.ID_PROCEDIMIENTO
                    where proc.ID_CITA == idCita
                    select uso).Any();
        }
    }
}

