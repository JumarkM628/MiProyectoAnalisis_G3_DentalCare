using DentalCare.Abstraccion.LogicaDeNegocio.Bitacora;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.LogicaDeNegocio.Bitacora.ObtenerEventos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BitacoraController : Controller
    {
        private readonly IObtenerEventosLN _obtenerLN;

        public BitacoraController()
        {
            _obtenerLN = new ObtenerEventosLN();
        }

        // GET: Bitacora/ObtenerTodosLosEventos
        public ActionResult ObtenerTodosLosEventos()
        {
            List<EventoDto> lista = _obtenerLN.Obtener();
            return View(lista);
        }
    }
}