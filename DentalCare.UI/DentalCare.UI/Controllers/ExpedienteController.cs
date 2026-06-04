using DentaCare.LogicaDeNegocio.Expedientes.CerrarExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.Alertas.GuardarAlerta;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.Alertas.ObtenerAlertasPorExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CerrarExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.ObtenerTodosLosExpedientes;
using DentalCare.Abstraccion.Modelo.Alertas;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Expedientes.CerrarExpediente;
using DentalCare.LogicaDeNegocio.Alertas.ObtenerAlertaPorExpediente;
using DentalCare.LogicaDeNegocio.Expedientes.Alertas.GuardarAlerta;
using DentalCare.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.LogicaDeNegocio.Expedientes.ObtenerTodosLosExpedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista,Doctor,Asistente")]
    public class ExpedienteController : Controller
    {
        private IObtenerTodosLosExpedientesLN _obtenerTodosLosExpedientesLN;
        private ICrearExpedienteLN _crearExpedienteLN;
        private ICerrarExpedienteLN _cerrarExpedienteLN;
        private IObtenerAlertaPorExpedienteLN _obtenerAlertaLN;
        private IGuardarAlertaLN _guardarAlertaLN;

        public ExpedienteController()
        {
            _obtenerTodosLosExpedientesLN = new ObtenerTodosLosExpedientesLN();
            _crearExpedienteLN = new CrearExpedienteLN();
            _cerrarExpedienteLN = new CerrarExpedienteLN(new CerrarExpedienteAD(new Contexto()));
            _guardarAlertaLN = new GuardarAlertaLN();
            _obtenerAlertaLN = new ObtenerAlertaPorExpedienteLN();
        }

        // GET: Expediente
        public ActionResult ObtenerTodosLosExpedientes()
        {
            List<ExpedienteDto> lista = _obtenerTodosLosExpedientesLN.Obtener();
            return View(lista);
        }

        // GET: Expediente/Details/5
        public ActionResult DetallesAlerta(int id)
        {
            ExpedienteDetalleDto detalle = _obtenerAlertaLN.Obtener(id);
            if (detalle == null)
            {
                TempData["Error"] = "No se encontró el expediente.";
                return RedirectToAction("ObtenerTodosLosExpedientes");
            }
            return View(detalle);
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

        public ActionResult Cerrar(int id)
        {
            var expediente = _cerrarExpedienteLN.ObtenerExpedientePorId(id);

            if (expediente == null)
                return HttpNotFound();

            if (expediente.IdEstado == 2)
            {
                TempData["Error"] = "El expediente ya se encuentra cerrado y no puede modificarse.";
                return RedirectToAction("ObtenerTodosLosExpedientes");
            }

            return View(expediente);
        }

        // POST: Expediente/Cerrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cerrar(int id, string confirmacion)
        {
            string nombreDoctora = User.Identity.Name;

            string error = _cerrarExpedienteLN.CerrarExpediente(id, nombreDoctora);

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Cerrar", new { id });
            }

            TempData["Exito"] = "El expediente fue cerrado correctamente.";
            return RedirectToAction("ObtenerTodosLosExpedientes");
        }

        public ActionResult GuardarAlerta(int id)
        {
            ExpedienteDetalleDto detalle = _obtenerAlertaLN.Obtener(id);
            if (detalle == null)
            {
                TempData["Error"] = "No se encontró el expediente.";
                return RedirectToAction("ObtenerTodosLosExpedientes");
            }

            AlertaDto dto = detalle.Alerta ?? new AlertaDto { IdExpediente = id };
            dto.IdExpediente = id;
            CargarDropdownsAlerta(dto);
            return View(dto);
        }

        // POST: Expediente/GuardarAlerta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarAlerta(int id, AlertaDto dto)
        {
            dto.IdExpediente = id;

            if (!ModelState.IsValid)
            {
                CargarDropdownsAlerta(dto);
                return View(dto);
            }

            string error = _guardarAlertaLN.Guardar(id, dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdownsAlerta(dto);
                return View(dto);
            }

            TempData["Exito"] = "Alerta médica guardada correctamente.";
            return RedirectToAction("DetallesAlerta", new { id });
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

        private AlertaDto CargarDropdownsAlerta(AlertaDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaEstados = ctx.Estados
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEstado.ToString(),
                        Text = e.NombreEstado
                    }).ToList();

                dto.ListaNivelesRiesgo = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Bajo",   Text = "Bajo"   },
                    new SelectListItem { Value = "Medio",  Text = "Medio"  },
                    new SelectListItem { Value = "Alto",   Text = "Alto"   },
                    new SelectListItem { Value = "Crítico",Text = "Crítico" }
                };
            }
            return dto;
        }
    }
}
