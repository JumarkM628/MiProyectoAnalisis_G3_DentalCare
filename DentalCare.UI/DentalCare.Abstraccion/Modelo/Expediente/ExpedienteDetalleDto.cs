using DentalCare.Abstraccion.Modelo.Alertas;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentalCare.Abstraccion.Modelo.Expedientes
{
    public class ExpedienteDetalleDto
    {
        public ExpedienteDto Expediente { get; set; }
        public AlertaDto Alerta { get; set; }

        public bool TieneAlertaActiva =>
            Alerta != null &&
            !string.IsNullOrEmpty(Alerta.NivelRiesgoMostrar) &&
            Alerta.IdEstado == 1;
    }
}
