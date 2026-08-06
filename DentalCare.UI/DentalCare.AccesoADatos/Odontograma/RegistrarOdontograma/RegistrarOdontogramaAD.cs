using System;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Odontograma.RegistrarOdontograma;
using DentalCare.Abstraccion.Modelo.Odontograma;

namespace DentalCare.AccesoADatos.Odontograma.RegistrarOdontograma
{
    public class RegistrarOdontogramaAD : IRegistrarOdontogramaAD
    {
        private readonly Contexto _contexto;

        public RegistrarOdontogramaAD()
        {
            _contexto = new Contexto();
        }

        public void Registrar(OdontogramaDto dto)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    // Verificar si el expediente ya tiene un odontograma
                    int idOdontogramaExistente = _contexto.Expedientes
                        .Where(e => e.IdExpediente == dto.IdExpediente)
                        .Select(e => e.IdOdontograma)
                        .FirstOrDefault();

                    int idOdontograma;

                    if (idOdontogramaExistente == 0)
                    {
                        // Caso nuevo: crear odontograma y vincularlo al expediente
                        idOdontograma = _contexto.Database
                            .SqlQuery<int>("SELECT ISNULL(MAX(ID_ODONTOGRAMA), 0) + 1 FROM FIDE_ODONTOGRAMA_TB")
                            .FirstOrDefault();

                        int idPiezaPrincipal = dto.Detalles != null && dto.Detalles.Count > 0
                            ? dto.Detalles[0].IdPieza : 0;

                        _contexto.Database.ExecuteSqlCommand(
                            @"INSERT INTO FIDE_ODONTOGRAMA_TB (ID_ODONTOGRAMA, FECHA, ID_PIEZA, ID_ESTADO)
                              VALUES (@p0, @p1, @p2, @p3)",
                            idOdontograma, DateTime.Now, idPiezaPrincipal, 1);

                        _contexto.Database.ExecuteSqlCommand(
                            @"UPDATE FIDE_EXPEDIENTE_TB SET ID_ODONTOGRAMA = @p0
                              WHERE ID_EXPEDIENTE = @p1",
                            idOdontograma, dto.IdExpediente);
                    }
                    else
                    {
                        // Caso existente: agregar detalles al odontograma ya registrado
                        idOdontograma = idOdontogramaExistente;
                    }

                    // Insertar los nuevos detalles (ID_DETALLE tiene IDENTITY)
                    foreach (var detalle in dto.Detalles)
                    {
                        _contexto.Database.ExecuteSqlCommand(
                            @"INSERT INTO FIDE_ODONTOGRAMA_DETALLE_TB
                              (ID_ODONTOGRAMA, ID_PIEZA, ESTADO_PIEZA)
                              VALUES (@p0, @p1, @p2)",
                            idOdontograma,
                            detalle.IdPieza,
                            detalle.EstadoPieza);
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
    }
}