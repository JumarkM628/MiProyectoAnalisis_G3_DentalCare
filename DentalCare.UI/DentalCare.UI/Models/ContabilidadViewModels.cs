using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Pagos;

namespace DentalCare.UI.Models
{
    // MDC-009: combina la lista completa de pagos (para la tabla + filtro) con
    // el pago seleccionado para edición.
    public class EditarPagoViewModel
    {
        public List<PagoDto> ListaPagos { get; set; }

        // Siempre viene con dropdowns cargados (aunque sea un PagoDto vacío) para
        // que el bloque del formulario exista en el HTML y el botón "Editar" de
        // cada fila solo tenga que mostrarlo/llenarlo por JS, sin recargar la página.
        public PagoDto PagoSeleccionado { get; set; }

        // Indica si PagoSeleccionado corresponde a un pago real (true) o es
        // solo la plantilla vacía usada para poder pre-renderizar el formulario oculto.
        public bool HayPagoSeleccionado { get; set; }
    }
}
