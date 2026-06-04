using System;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Odontograma.RegistrarOdontograma;
using DentalCare.Abstraccion.Modelo.Odontograma;
using DentalCare.AccesoADatos.Entidades.Odontograma;

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
            var odontograma = new OdontogramaEntidad
            {
                Fecha = DateTime.Now,
                IdEstado = 1
            };

            _contexto.Odontogramas.Add(odontograma);
            _contexto.SaveChanges();

            foreach (var detalle in dto.Detalles)
            {
                var detalleEntidad = new OdontogramaDetalleEntidad
                {
                    IdOdontograma = odontograma.IdOdontograma,
                    IdPieza = detalle.IdPieza,
                    EstadoPieza = detalle.EstadoPieza
                };

                _contexto.OdontogramaDetalles.Add(detalleEntidad);
            }

            _contexto.SaveChanges();
        }
    }
}