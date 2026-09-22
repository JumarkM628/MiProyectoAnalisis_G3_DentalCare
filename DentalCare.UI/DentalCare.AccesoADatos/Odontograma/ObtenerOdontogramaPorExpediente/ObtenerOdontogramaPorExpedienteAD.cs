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
            // Paso 1: obtener el ID_ODONTOGRAMA del expediente
            var idOdontograma = _contexto.Expedientes
                .Where(e => e.IdExpediente == idExpediente)
                .Select(e => e.IdOdontograma)
                .FirstOrDefault();

            if (idOdontograma == 0) return null;

            // Paso 2: obtener detalles a memoria
            var rawDetalles = (
                from detalle in _contexto.OdontogramaDetalles
                where detalle.IdOdontograma == idOdontograma
                join pieza in _contexto.PiezasDentales
                    on detalle.IdPieza equals pieza.IdPieza
                select new
                {
                    detalle.IdDetalle,
                    detalle.IdPieza,
                    pieza.NumeroPieza,
                    detalle.EstadoPieza
                }
            ).ToList();

            var detalles = rawDetalles.Select(d => new OdontogramaDetalleDto
            {
                IdDetalle = d.IdDetalle,
                IdPieza = d.IdPieza,
                NumeroPieza = d.NumeroPieza,
                EstadoPieza = d.EstadoPieza
            }).ToList();

            return new OdontogramaDto
            {
                IdOdontograma = idOdontograma,
                IdExpediente = idExpediente,
                Detalles = detalles
            };
        }
    }
}