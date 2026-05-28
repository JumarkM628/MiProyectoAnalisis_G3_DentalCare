using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.ObtenerTodosLosExpedientes;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos;
using DentalCare.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.LogicaDeNegocio.Expedientes.ObtenerTodosLosExpedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    public class ExpedienteController : Controller
    {
        private IObtenerTodosLosExpedientesLN _obtenerTodosLosExpedientesLN;
        private ICrearExpedienteLN _crearExpedienteLN;

        public ExpedienteController()
        {
            _obtenerTodosLosExpedientesLN = new ObtenerTodosLosExpedientesLN();
            _crearExpedienteLN = new CrearExpedienteLN();
        }


        // GET: Expediente
        public ActionResult ObtenerTodosLosExpedientes()
        {
            List<ExpedienteDto> lista = _obtenerTodosLosExpedientesLN.Obtener();
            return View(lista);
        }

        // GET: Expediente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expediente/Create
        public ActionResult CrearExpediente()
        {
            var dto = CargarDropdowns(new ExpedienteDto());
            return View(dto);
        }

        // POST: Expediente/Create
        [HttpPost]
        public ActionResult CrearExpediente(ExpedienteDto dto)
        {
            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _crearExpedienteLN.Crear(dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "Expediente creado correctamente.";
            return RedirectToAction("ObtenerTodosLosExpedientes");
        }

        // GET: Expediente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Expediente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Expediente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Expediente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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

        private ExpedienteDto CargarDropdowns(ExpedienteDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaEstados = ctx.Estados
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEstado.ToString(),
                        Text = e.NombreEstado
                    }).ToList();
            }
            return dto;
        }
    }
}
