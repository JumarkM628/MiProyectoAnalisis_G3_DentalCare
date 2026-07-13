using DentalCare.Abstraccion.AccesoADatos.Tratamiento.AgregarTratamiento;
using DentalCare.Abstraccion.Modelo.Tratamientos;
using System;
using System.Linq;

namespace DentalCare.AccesoADatos.Tratamientos.RegistrarTratamiento
{
    public class RegistrarTratamientoAD : IRegistrarTratamientoAD
    {
        private readonly Contexto _contexto;

        public RegistrarTratamientoAD()
        {
            _contexto = new Contexto();
        }

        /// <summary>
        /// Escenario 1 y 3: registra un tratamiento vinculado a la cita.
        /// Múltiples llamadas a este método registran múltiples tratamientos
        /// para la misma cita, acumulando el total.
        /// </summary>
        public void Registrar(TratamientoDto dto)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    int nuevoId = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_TRATAMIENTO), 0) + 1 FROM FIDE_TRATAMIENTO_TB")
                        .FirstOrDefault();

                    _contexto.Database.ExecuteSqlCommand(
                        @"INSERT INTO FIDE_TRATAMIENTO_TB
                          (ID_TRATAMIENTO, DESCRIPCION, FECHA_INICIO, FECHA_FIN, MONTO, ID_CITA, ID_ESTADO)
                          VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6)",
                        nuevoId,
                        dto.Descripcion,
                        dto.FechaInicioForm ?? DateTime.Now,
                        dto.FechaFinForm,
                        dto.Monto,
                        dto.IdCitaForm,
                        dto.IdEstado);

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public bool ExisteCita(int idCita)
        {
            return _contexto.Citas.Any(c => c.IdCita == idCita);
        }
    }
}