using DentalCare.Abstraccion.AccesoADatos.Gastos.ActualizarEstadoPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using System.Linq;

namespace DentalCare.AccesoADatos.Pago.ActualizarEstadoPago
{
    public class ActualizarEstadoPagoAD : IActualizarEstadoPagoAD
    {
        private readonly Contexto _contexto;

        // ID de estado "Pendiente" y "Activo" (pagado)
        private const int ESTADO_ACTIVO = 1;
        private const int ESTADO_PENDIENTE = 5;

        public ActualizarEstadoPagoAD()
        {
            _contexto = new Contexto();
        }

        /// <summary>
        /// MDC-004 y MDC-005: actualiza el estado del pago en FIDE_CONTABILIDAD_TB.
        /// </summary>
        public void Actualizar(int idContabilidad, int idEstado)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var contabilidad = _contexto.Contabilidades
                        .First(c => c.IdContabilidad == idContabilidad);

                    contabilidad.IdEstado = idEstado;
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

        public bool ExistePago(int idContabilidad)
        {
            return _contexto.Contabilidades
                .Any(c => c.IdContabilidad == idContabilidad);
        }

        // MDC-005: verificar si ya está pagado para evitar duplicado
        public bool EstaPagado(int idContabilidad)
        {
            return _contexto.Contabilidades
                .Any(c => c.IdContabilidad == idContabilidad
                       && c.IdEstado == ESTADO_ACTIVO);
        }

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
                    on cont.IdMetodoPago equals metodo.IdMetodoPago

                join estado in _contexto.Estados
                    on cont.IdEstado equals estado.IdEstado

                select new
                {
                    cont.IdContabilidad,
                    cont.Monto,
                    cont.Fecha,
                    cont.IdEstado,
                    IdCita = cc.IdCita,
                    NombreMetodo = metodo.Descripcion,
                    NombreEstado = estado.NombreEstado,
                    NombrePaciente = paciente != null
                        ? paciente.Nombre + " " + paciente.PrimerApellido
                        : "Sin paciente"
                }
            ).FirstOrDefault();

            if (rawData == null) return null;

            return new PagoDto
            {
                IdContabilidad = rawData.IdContabilidad,
                Monto = rawData.Monto ?? 0,
                FechaPago = rawData.Fecha,
                IdCita = rawData.IdCita,
                IdEstado = rawData.IdEstado,
                NombreMetodoPago = rawData.NombreMetodo,
                NombreEstado = rawData.NombreEstado,
                NombrePaciente = rawData.NombrePaciente
            };
        }
    }
}