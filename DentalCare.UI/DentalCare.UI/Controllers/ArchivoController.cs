using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DentalCare.Abstraccion.LogicaDeNegocio.Archivo;
using DentalCare.Abstraccion.Modelo.Archivo;

namespace DentalCare.UI.Controllers
{
    public class ArchivoController : Controller
    {
        private readonly IArchivoClinicoLN _archivoClinicoLN;

        public ArchivoController(IArchivoClinicoLN archivoClinicoLN)
        {
            _archivoClinicoLN = archivoClinicoLN;
        }

        // GET: Archivo/Subir/5
        public ActionResult Subir(int expedienteId)
        {
            var dto = new ArchivoClinicoDto
            {
                ExpedienteId = expedienteId
            };
            return View(dto);
        }

        // POST: Archivo/Subir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subir(ArchivoClinicoDto model, HttpPostedFileBase archivo)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UsuarioId = (int)Session["UsuarioId"];

            string error = _archivoClinicoLN.SubirArchivo(model, archivo);

            if (error != null)
            {
                ModelState.AddModelError("", error);
                return View(model);
            }

            TempData["Exito"] = "El archivo fue cargado correctamente.";
            return RedirectToAction("Subir", new { expedienteId = model.ExpedienteId });
        }
    }
}