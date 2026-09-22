using DentalCare.Abstraccion.AccesoADatos.Pago.EditarPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using DentalCare.AccesoADatos.Entidades.Bitacora;
using System;
using System.Linq;

namespace DentalCare.AccesoADatos.Pago.EditarPago
{
    public class EditarPagoAD : IEditarPagoAD
    {
        private readonly Contexto _contexto;

        // ID de estado "Activo" (Pagado) — mismo valor usado en ActualizarEstadoPagoAD
        private const int ESTADO_PAGADO = 1;

        public EditarPagoAD()
        {
            _contexto = new Contexto();
        }

        /// <summary>
        /// MDC-009: arma el mismo join usado en ActualizarEstadoPagoAD.ObtenerPorId,
        /// agregando el nombre del tratamiento vía FIDE_CONTABILIDAD_CITA_TB.ID_CITA
        /// -> FIDE_TRATAMIENTO_TB.ID_CITA para precargar el formulario de edición.
        /// </summary>
        public PagoDto ObtenerPorId(int idContabilidad)
        {
            var rawData = (
                from cont in _contexto.Contabilidades
                where cont.IdContabilidad == idContabilidad

                join cc in _contexto.ContabilidadCitas
                    on cont.IdContabilidad equals cc.IdContabilidad

                join cp in _contexto.ContabilidadPacientes
                    on cont.IdContabilidad equals cp.IdContabilidad into cpGrupo
                from cp in cpGrupo.DefaultIfEmpty()

                join paciente in _contexto.Usuarios
                    on cp.IdUsuario equals paciente.IdUsuario into pacienteGrupo
                from paciente in pacienteGrupo.DefaultIfEmpty()

                join metodo in _contexto.MetodosPago
                    on cont.IdMetodoPago equals (int?)metodo.IdMetodoPago into metodoGrupo
                from metodo in metodoGrupo.DefaultIfEmpty()

                join estado in _contexto.Estados
                    on cont.IdEstado equals estado.IdEstado

                select new
                {
                    cont.IdContabilidad,
                    cont.Monto,
                    cont.Fecha,
                    cont.IdEstado,
                    cont.IdMetodoPago,
                    IdCita = cc.IdCita,
                    NombreMetodo = metodo != null ? metodo.Descripcion : "Sin método",
                    NombreEstado = estado.NombreEstado,
                    NombrePaciente = paciente != null
                        ? paciente.Nombre + " " + paciente.PrimerApellido
                        : "Sin paciente"
                }
            ).FirstOrDefault();

            if (rawData == null) return null;

            var tratamientos = _contexto.PlanesTratamiento
                .Where(t => t.IdCita == rawData.IdCita)
                .Select(t => t.Descripcion)
                .ToList();

            string nombreTratamiento = tratamientos.Any()
                ? string.Join(", ", tratamientos)
                : "Sin tratamiento asociado";

            return new PagoDto
            {
                IdContabilidad = rawData.IdContabilidad,
                Monto = rawData.Monto ?? 0,
                FechaPago = rawData.Fecha,
                IdCita = rawData.IdCita,
                IdEstado = rawData.IdEstado,
                IdMetodoPago = rawData.IdMetodoPago,
                NombreMetodoPago = rawData.NombreMetodo,
                NombreEstado = rawData.NombreEstado,
                NombrePaciente = rawData.NombrePaciente,
                NombreTratamiento = nombreTratamiento
            };
        }

        public bool ExistePago(int idContabilidad)
        {
            return _contexto.Contabilidades
                .Any(c => c.IdContabilidad == idContabilidad);
        }

        // Escenario 5: un pago con ID_ESTADO = 1 (Activo/Pagado) ya está cerrado
        public bool EstaPagado(int idContabilidad)
        {
            return _contexto.Contabilidades
                .Any(c => c.IdContabilidad == idContabilidad
                       && c.IdEstado == ESTADO_PAGADO);
        }

        /// <summary>
        /// MDC-009: actualiza Monto, Método de pago, Fecha y Estado en FIDE_CONTABILIDAD_TB
        /// y registra el cambio en Bitacora dentro de la misma transacción, para que
        /// ambos se confirmen o se reviertan juntos.
        /// </summary>
        public void Actualizar(PagoDto dto, string nombreUsuario)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var contabilidad = _contexto.Contabilidades
                        .First(c => c.IdContabilidad == dto.IdContabilidad);

                    decimal montoAnterior = contabilidad.Monto ?? 0;

                    contabilidad.Monto = dto.Monto;
                    contabilidad.IdMetodoPago = dto.IdMetodoPago;
                    contabilidad.Fecha = dto.FechaPago;
                    contabilidad.IdEstado = dto.IdEstado;

                    var bitacora = new BitacoraEntidad
                    {
                        Modulo = "Contabilidad",
                        Accion = "Edición de pago",
                        Descripcion = $"Se editó el pago ID {dto.IdContabilidad}. " +
                                         $"Monto anterior: {montoAnterior:N2} -> Monto nuevo: {dto.Monto:N2}.",
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
    }
}
