using DentalCare.Abstraccion.AccesoADatos.Pago.RegistrarPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Pago.RegistroPago
{
    public class RegistrarPagoAD : IRegistrarPagoAD
    {
        private readonly Contexto _contexto;
        private const int ID_GASTO_PLACEHOLDER = 1; // Insertado en BD como dato base

        public RegistrarPagoAD()
        {
            _contexto = new Contexto();
        }

        /// <summary>
        /// Escenario 1: registra el pago en cascada:
        /// FIDE_ANTICIPO_TB → FIDE_CONTABILIDAD_TB →
        /// FIDE_CONTABILIDAD_CITA_TB → FIDE_CONTABILIDAD_PACIENTE_TB
        /// </summary>
        public void Registrar(PagoDto dto)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    // Obtener IdUsuario del paciente vinculado a la cita
                    int idPaciente = _contexto.UsuarioCitas
                        .Where(uc => uc.IdCita == dto.IdCitaForm)
                        .Select(uc => uc.IdUsuario)
                        .FirstOrDefault();

                    // IDs para los nuevos registros
                    int nuevoIdAnticipo = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_ANTICIPO), 0) + 1 FROM FIDE_ANTICIPO_TB")
                        .FirstOrDefault();

                    int nuevoIdContabilidad = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_CONTABILIDAD), 0) + 1 FROM FIDE_CONTABILIDAD_TB")
                        .FirstOrDefault();

                    // 1. Insertar anticipo (monto + método de pago)
                    _contexto.Database.ExecuteSqlCommand(
                        @"INSERT INTO FIDE_ANTICIPO_TB
                          (ID_ANTICIPO, ID_METODO_PAGO, FECHA, MONTO, ID_ESTADO)
                          VALUES (@p0, @p1, @p2, @p3, @p4)",
                        nuevoIdAnticipo,
                        dto.IdMetodoPago,
                        DateTime.Now,
                        dto.Monto,
                        dto.IdEstado);

                    // 2. Insertar contabilidad (une anticipo, gasto placeholder y método)
                    _contexto.Database.ExecuteSqlCommand(
                        @"INSERT INTO FIDE_CONTABILIDAD_TB
                          (ID_CONTABILIDAD, MONTO, ID_METODO_PAGO, ID_GASTO, ID_ANTICIPO, FECHA, ID_ESTADO)
                          VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6)",
                        nuevoIdContabilidad,
                        dto.Monto,
                        dto.IdMetodoPago,
                        ID_GASTO_PLACEHOLDER,
                        nuevoIdAnticipo,
                        DateTime.Now,
                        dto.IdEstado);

                    // 3. Vincular contabilidad con la cita
                    _contexto.Database.ExecuteSqlCommand(
                        @"INSERT INTO FIDE_CONTABILIDAD_CITA_TB
                          (ID_CONTABILIDAD, ID_CITA) VALUES (@p0, @p1)",
                        nuevoIdContabilidad,
                        dto.IdCitaForm);

                    // 4. Vincular contabilidad con el paciente (Escenario 5)
                    if (idPaciente > 0)
                    {
                        _contexto.Database.ExecuteSqlCommand(
                            @"INSERT INTO FIDE_CONTABILIDAD_PACIENTE_TB
                              (ID_USUARIO, ID_CONTABILIDAD) VALUES (@p0, @p1)",
                            idPaciente,
                            nuevoIdContabilidad);
                    }

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        // Verificar que la cita esté finalizada antes de cobrar
        public bool ExisteCitaFinalizada(int idCita)
        {
            var estadoFinalizada = _contexto.Estados
                .FirstOrDefault(e => e.NombreEstado == "Finalizada");

            if (estadoFinalizada == null) return false;

            return _contexto.Citas
                .Any(c => c.IdCita == idCita && c.IdEstado == estadoFinalizada.IdEstado);
        }

        // Evitar pago duplicado para la misma cita
        public bool YaTienePago(int idCita)
        {
            return _contexto.ContabilidadCitas
                .Any(cc => cc.IdCita == idCita);
        }
    }
}
