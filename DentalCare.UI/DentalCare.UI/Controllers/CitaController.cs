using DentalCare.Abstraccion.Modelo.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    public class CitaController : Controller
    {
        // GET: Cita
        public ActionResult ObtenerTodasLasCitas()
        {
            var lista = new List<CitaTDO>()
    {
        new CitaTDO
        {
            Id_cita = 1,
            descripcion = "Consulta",
            Fecha_Cita = DateTime.Now.AddDays(1),
            Fecha_Registro = DateTime.Now,
            Fecha_Modificacion = null,
            Estado = true,
            Id_Usuario = 1,
            Id_Doctor = 1
        },
        new CitaTDO
        {
            Id_cita = 2,
            descripcion = "Limpieza",
            Fecha_Cita = DateTime.Now.AddDays(2),
            Fecha_Registro = DateTime.Now,
            Fecha_Modificacion = null,
            Estado = true,
            Id_Usuario = 2,
            Id_Doctor = 1
        }
    };

            return View(lista);
        }

        // GET: Cita/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cita/Create
        public ActionResult AgregarCita()
        {
            return View();
        }

        // POST: Cita/Create
        [HttpPost]
        public ActionResult AgregarCita(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cita/Edit/5
        public ActionResult EditarCita(int id)
        {
            return View();
        }

        // POST: Cita/Edit/5
        [HttpPost]
        public ActionResult EditarCita(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cita/Delete/5
        public ActionResult EliminarCita(int id)
        {
            return View();
        }

        // POST: Cita/Delete/5
        [HttpPost]
        public ActionResult EliminarCita(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
