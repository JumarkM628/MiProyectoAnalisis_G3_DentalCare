using DentalCare.Abstraccion.AccesoADatos.Gastos;
using DentalCare.Abstraccion.Modelo.Gasto;
using DentalCare.AccesoADatos.Entidades.Bitacora;
using System;
using System.Data.Entity;
using System.Linq;

namespace DentalCare.AccesoADatos.Gastos.EditarGasto
{
    public class EditarGastoAD : IEditarGastoAD
    {
        private readonly Contexto _contexto;

        public EditarGastoAD()
        {
            _contexto = new Contexto();
        }

        public GastoDto ObtenerGastoPorId(int id)
        {
            var entidad = _contexto.Gastos.FirstOrDefault(g => g.IdGasto == id);
            if (entidad == null) return null;

            return new GastoDto
            {
                IdGasto = entidad.IdGasto,
                TipoGasto = ExtraerCampo(entidad.Descripcion, "TIPO"),
                Concepto = ExtraerCampo(entidad.Descripcion, "CONCEPTO"),
                Monto = entidad.Monto ?? 0,
                Fecha = entidad.Fecha,
                IdEstado = entidad.IdEstado
            };
        }

        /// <summary>
        /// Escenario MDC-006: actualiza el gasto en FIDE_GASTO_TB.
        /// La categoría/concepto se re-concatenan en DESCRIPCION con el mismo
        /// formato "TIPO:x|CONCEPTO:x" usado en RegistrarGastoAD.
        /// Registra el cambio en Bitacora dentro de la misma transacción del
        /// UPDATE, para que ambos se confirmen o se reviertan juntos.
        /// </summary>
        public void Editar(GastoDto dto, string nombreUsuario)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var gastoAnterior = _contexto.Gastos
                        .AsNoTracking()
                        .First(g => g.IdGasto == dto.IdGasto);

                    decimal montoAnterior = gastoAnterior.Monto ?? 0;
                    string descripcionAnterior = gastoAnterior.Descripcion;

                    string descripcionConcatenada =
                        $"TIPO:{dto.TipoGasto}|CONCEPTO:{dto.Concepto}";

                    _contexto.Database.ExecuteSqlCommand(
                        @"UPDATE FIDE_GASTO_TB
                          SET DESCRIPCION = @p1, MONTO = @p2, FECHA = @p3, ID_ESTADO = @p4
                          WHERE ID_GASTO = @p0",
                        dto.IdGasto,
                        descripcionConcatenada,
                        dto.Monto,
                        dto.Fecha ?? DateTime.Now,
                        dto.IdEstado);

                    var bitacora = new BitacoraEntidad
                    {
                        Modulo = "Contabilidad",
                        Accion = "Edición de gasto",
                        Descripcion = $"Se editó el gasto ID {dto.IdGasto}. " +
                                         $"Monto anterior: {montoAnterior:N2} -> Monto nuevo: {dto.Monto:N2}. " +
                                         $"Descripción anterior: '{descripcionAnterior}' -> Descripción nueva: '{descripcionConcatenada}'.",
                        NombreUsuario = nombreUsuario,
                        FechaHora = DateTime.Now
                    };
                    _contexto.Bitacoras.Add(bitacora);
                    _contexto.SaveChanges();

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        private static string ExtraerCampo(string descripcion, string campo)
        {
            if (string.IsNullOrEmpty(descripcion)) return string.Empty;
            foreach (var parte in descripcion.Split('|'))
                if (parte.StartsWith(campo + ":"))
                    return parte.Substring((campo + ":").Length).Trim();
            return descripcion; // Si no tiene formato, mostrar tal cual
        }
    }
}
