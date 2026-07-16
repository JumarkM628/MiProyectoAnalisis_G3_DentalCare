using DentalCare.Abstraccion.AccesoADatos.Catalogo.ActualizarCosto;
using DentalCare.Abstraccion.Modelo.Catalogo;
using System;
using System.Linq;

namespace DentalCare.AccesoADatos.Catalogo.ActualizarCosto
{
    public class ActualizarCostoAD : IActualizarCostoAD
    {
        private readonly Contexto _contexto;

        public ActualizarCostoAD()
        {
            _contexto = new Contexto();
        }

        public void Actualizar(int idCatalogo, CatalogoDto dto)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var catalogo = _contexto.CatalogoTratamientos
                        .First(c => c.IdCatalogo == idCatalogo);

                    // Guardar costo anterior antes de actualizar
                    catalogo.CostoAnterior = catalogo.Costo;
                    catalogo.Costo = dto.NuevoCosto;
                    catalogo.FechaActualizacion = DateTime.Now;

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