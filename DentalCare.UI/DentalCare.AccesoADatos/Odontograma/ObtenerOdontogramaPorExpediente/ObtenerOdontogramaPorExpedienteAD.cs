using DentalCare.Abstraccion.AccesoADatos.Odontograma.ObtenerOdontogramaPorExpediente;
using DentalCare.Abstraccion.Modelo.Odontograma;

using System.Linq;

namespace DentalCare.AccesoADatos.Odontograma.ObtenerOdontogramaPorExpediente
{
    public class ObtenerOdontogramaPorExpedienteAD : IObtenerOdontogramaPorExpedienteAD
    {
        private readonly Contexto _contexto;

        public ObtenerOdontogramaPorExpedienteAD()
        {
            _contexto = new Contexto();
        }

        public OdontogramaDto Obtener(int idExpediente)
        {
            var odontograma = _contexto.Expedientes
                .Where(e => e.IdExpediente == idExpediente)
                .Select(e => e.IdOdontograma)
                .FirstOrDefault();

            if (odontograma == 0) return null;

            var detalles = (
                from detalle in _contexto.OdontogramaDetalles
                where detalle.IdOdontograma == odontograma
                join pieza in _contexto.PiezasDentales
                    on detalle.IdPieza equals pieza.IdPieza
                select new OdontogramaDetalleDto
                {
                    IdDetalle = detalle.IdDetalle,
                    IdPieza = detalle.IdPieza,
                    NumeroPieza = pieza.NumeroPieza,
                    EstadoPieza = detalle.EstadoPieza
                }
            ).ToList();

            return new OdontogramaDto
            {
                IdOdontograma = odontograma,
                IdExpediente = idExpediente,
                Detalles = detalles
            };
        }
    }
}