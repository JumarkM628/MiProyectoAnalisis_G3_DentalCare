using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DentaCare.LogicaDeNegocio.Citas.ObtenerTodasLasCitas;

namespace DentalCare.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Recordatorio de citas del día para el personal que ya inició sesión
            if (User.Identity.IsAuthenticated && !User.IsInRole("Paciente"))
            {
                try
                {
                    var todas = new ObtenerTodasLasCitasLN().Obtener();
                    ViewBag.CitasHoy = CitaController.FiltrarCitasDeHoy(todas);
                }
                catch
                {
                    ViewBag.CitasHoy = null;
                }
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}