using DentalCare.Abstraccion.AccesoADatos.Gastos;
using DentalCare.Abstraccion.Modelo.Gasto;
using System;
using System.Linq;

namespace DentalCare.AccesoADatos.Gastos.RegistrarGasto
{
    public class RegistrarGastoAD : IRegistrarGastoAD
    {
        private readonly Contexto _contexto;

        public RegistrarGastoAD()
        {
            _contexto = new Contexto();
        }

        /// <summary>
        /// Escenario 1: registra el gasto en FIDE_GASTO_TB.
        /// Escenario 3: la categoría se concatena en DESCRIPCION con el formato
        /// "TIPO:x|CONCEPTO:x" igual que hicimos con consentimiento y alertas.
        /// </summary>
        public void Registrar(GastoDto dto)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    int nuevoId = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_GASTO), 0) + 1 FROM FIDE_GASTO_TB")
                        .FirstOrDefault();

                    // Concatenar tipo/categoría y concepto en DESCRIPCION
                    string descripcionConcatenada =
                        $"TIPO:{dto.TipoGasto}|CONCEPTO:{dto.Concepto}";

                    _contexto.Database.ExecuteSqlCommand(
                        @"INSERT INTO FIDE_GASTO_TB
                          (ID_GASTO, DESCRIPCION, MONTO, FECHA, ID_ESTADO)
                          VALUES (@p0, @p1, @p2, @p3, @p4)",
                        nuevoId,
                        descripcionConcatenada,
                        dto.Monto,
                        dto.Fecha ?? DateTime.Now,
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
    }
}