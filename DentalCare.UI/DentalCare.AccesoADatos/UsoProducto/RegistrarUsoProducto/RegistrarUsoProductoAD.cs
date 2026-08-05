using System;
using System.Collections.Generic;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.UsoProducto;
using DentalCare.Abstraccion.Modelo.Producto.UsoProducto;
using DentalCare.AccesoADatos.Entidades.Procedimiento;
using DentalCare.AccesoADatos.Entidades.Producto;
using DentalCare.AccesoADatos.Entidades.Tratamientos;

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

            // Start transaction to create treatment + procedure atomically
            using (var tx = _contexto.Database.BeginTransaction())
            {
                try
                {
                    // Create a treatment record to satisfy FK
                    int nuevoTratamientoId = _contexto.PlanesTratamiento.Any()
                        ? _contexto.PlanesTratamiento.Max(t => t.IdTratamiento) + 1
                        : 1;

                    var nuevoTratamiento = new PlanTratamientoEntidad
                    {
                        IdTratamiento = nuevoTratamientoId,
                        Descripcion = "Tratamiento generado automáticamente para procedimiento",
                        FechaInicio = DateTime.Now,
                        FechaFin = null,
                        Monto = 0,
                        IdCita = idCita,
                        IdEstado = 1
                    };

                    _contexto.PlanesTratamiento.Add(nuevoTratamiento);
                    _contexto.SaveChanges();

                    // Now create procedimiento and reference the new treatment
                    int nuevoId = _contexto.Procedimientos.Any()
                        ? _contexto.Procedimientos.Max(p => p.ID_PROCEDIMIENTO) + 1
                        : 1;

                    var nuevo = new ProcedimientoEntidad
                    {
                        ID_PROCEDIMIENTO = nuevoId,
                        ID_CITA = idCita,
                        ID_TRATAMIENTO = nuevoTratamiento.IdTratamiento,
                        DESCRIPCION = "Procedimiento generado automáticamente",
                        FECHA = DateTime.Now,
                        ID_ESTADO = 1
                    };

                    _contexto.Procedimientos.Add(nuevo);
                    _contexto.SaveChanges();

                    tx.Commit();
                    return nuevo.ID_PROCEDIMIENTO;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
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
            int nuevoIdUso = _contexto.UsoProductos.Any()
            ? _contexto.UsoProductos.Max(u => u.ID_USO) + 1
            : 1;
            var entidad = new UsoProductoEntidad
            {
                ID_USO = nuevoIdUso,
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

