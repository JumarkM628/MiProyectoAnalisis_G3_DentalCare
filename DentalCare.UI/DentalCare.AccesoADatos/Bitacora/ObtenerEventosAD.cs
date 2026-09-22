using DentalCare.Abstraccion.AccesoADatos.Bitacora;
using DentalCare.Abstraccion.Modelo.Bitacora;
using System.Collections.Generic;
using System.Linq;

namespace DentalCare.AccesoADatos.Bitacora.ObtenerEventos
{
    public class ObtenerEventosAD : IObtenerEventosAD
    {
        private readonly Contexto _contexto;

        public ObtenerEventosAD()
        {
            _contexto = new Contexto();
        }

        public List<EventoDto> Obtener()
        {
            var rawData = _contexto.Eventos
                .OrderByDescending(e => e.FechaDeEvento)
                .ToList();

            return rawData.Select(e => new EventoDto
            {
                IdEvento = e.IdEvento,
                TablaDeEvento = e.TablaDeEvento,
                TipoDeEvento = e.TipoDeEvento,
                FechaDeEvento = e.FechaDeEvento,
                DescripcionDeEvento = e.DescripcionDeEvento,
                StackTrace = e.StackTrace,
                DatosAnteriores = e.DatosAnteriores,
                DatosPosteriores = e.DatosPosteriores
            }).ToList();
        }
    }
}