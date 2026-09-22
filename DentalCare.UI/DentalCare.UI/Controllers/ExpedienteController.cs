using DentaCare.LogicaDeNegocio.Expedientes.CerrarExpediente;
using DentaCare.LogicaDeNegocio.Reporteria.Expediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.Alertas.GuardarAlerta;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.Alertas.ObtenerAlertasPorExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CerrarExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.ObtenerTodosLosExpedientes;
using DentalCare.Abstraccion.LogicaDeNegocio.Odontograma.ObtenerOdontogramaPorExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Odontograma.RegistrarOdontograma;
using DentalCare.Abstraccion.Modelo.Alertas;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.Abstraccion.Modelo.Odontograma;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Expedientes.CerrarExpediente;
using DentalCare.LogicaDeNegocio.Alertas.ObtenerAlertaPorExpediente;
using DentalCare.LogicaDeNegocio.Expedientes.Alertas.GuardarAlerta;
using DentalCare.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.LogicaDeNegocio.Expedientes.ObtenerTodosLosExpedientes;
using DentalCare.LogicaDeNegocio.Odontograma.ObtenerOdontogramaPorExpediente;
using DentalCare.LogicaDeNegocio.Odontograma.RegistrarOdontograma;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRegistrarOdontogramaLN _registrarOdontogramaLN;
        private readonly IObtenerOdontogramaPorExpedienteLN _obtenerOdontogramaLN;

        public ExpedienteController()
        {
            _obtenerTodosLosExpedientesLN = new ObtenerTodosLosExpedientesLN();
            _crearExpedienteLN = new CrearExpedienteLN();
            _cerrarExpedienteLN = new CerrarExpedienteLN(new CerrarExpedienteAD(new Contexto()));
            _guardarAlertaLN = new GuardarAlertaLN();
            _obtenerAlertaLN = new ObtenerAlertaPorExpedienteLN();
            _registrarOdontogramaLN = new RegistrarOdontogramaLN();
            _obtenerOdontogramaLN = new ObtenerOdontogramaPorExpedienteLN();
        }

        public ActionResult ObtenerTodosLosExpedientes()
        {
            List<ExpedienteDto> lista = _obtenerTodosLosExpedientesLN.Obtener();
            return View(lista);
        }

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

        public ActionResult CrearExpediente()
        {
            var dto = CargarDropdowns(new ExpedienteDto());
            return View(dto);
        }

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

        public ActionResult Edit(int id) => View();
        [HttpPost] public ActionResult Edit(int id, FormCollection c) { try { return RedirectToAction("Index"); } catch { return View(); } }
        public ActionResult Delete(int id) => View();
        [HttpPost] public ActionResult Delete(int id, FormCollection c) { try { return RedirectToAction("Index"); } catch { return View(); } }

        public ActionResult ProcedimientosExpediente(int idExpediente, DateTime? desde, DateTime? hasta, int? idTratamiento)
        {
            try
            {
                var ln = new ReporteExpedienteLN();
                var procedimientos = ln.ObtenerProcedimientosPorExpediente(idExpediente, desde, hasta, idTratamiento);
                // El trigger trg_Expediente_Update registra los cambios automáticamente — BitacoraLN eliminado

                var vm = new Abstraccion.Modelo.Expediente.ProcedimientosExpedienteModelDto
                {
                    IdExpediente = idExpediente,
                    Desde = desde,
                    Hasta = hasta,
                    IdTratamiento = idTratamiento,
                    Procedimientos = procedimientos
                };
                return View("ProcedimientosExpediente", vm);
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("DetallesAlerta", new { id = idExpediente });
            }
            catch
            {
                TempData["Error"] = "Ocurrió un error al obtener los procedimientos.";
                return RedirectToAction("DetallesAlerta", new { id = idExpediente });
            }
        }

        public ActionResult Cerrar(int id)
        {
            var expediente = _cerrarExpedienteLN.ObtenerExpedientePorId(id);
            if (expediente == null) return HttpNotFound();

            if (expediente.IdEstado == 2)
            {
                TempData["Error"] = "El expediente ya se encuentra cerrado y no puede modificarse.";
                return RedirectToAction("ObtenerTodosLosExpedientes");
            }
            return View(expediente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cerrar(int id, string confirmacion)
        {
            string error = _cerrarExpedienteLN.CerrarExpediente(id, User.Identity.Name);
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

        public ActionResult RegistrarOdontograma(int id)
        {
            // Si ya existe odontograma, cargar los detalles existentes para mostrarlos
            var existente = _obtenerOdontogramaLN.Obtener(id);
            var dto = existente ?? new OdontogramaDto { IdExpediente = id };
            dto.IdExpediente = id;
            // Limpiar detalles para el formulario de nuevos — los existentes se muestran en la vista
            dto.Detalles = new System.Collections.Generic.List<OdontogramaDetalleDto>();
            CargarDropdownsOdontograma(dto);
            ViewBag.DetallesExistentes = existente?.Detalles;
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarOdontograma(int id, OdontogramaDto dto)
        {
            dto.IdExpediente = id;
            if (!ModelState.IsValid)
            {
                CargarDropdownsOdontograma(dto);
                return View(dto);
            }
            string error = _registrarOdontogramaLN.Registrar(dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdownsOdontograma(dto);
                return View(dto);
            }
            TempData["Exito"] = "Odontograma registrado correctamente.";
            return RedirectToAction("ObtenerTodosLosExpedientes", new { id });
        }

        public ActionResult VerOdontograma(int id)
        {
            var dto = _obtenerOdontogramaLN.Obtener(id);
            if (dto == null)
            {
                TempData["Error"] = "No se encontró odontograma para este expediente.";
                return RedirectToAction("ObtenerTodosLosExpedientes");
            }
            return View(dto);
        }

        private ExpedienteDto CargarDropdowns(ExpedienteDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaEstados = ctx.Estados.Select(e => new SelectListItem
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
                dto.ListaEstados = ctx.Estados.Select(e => new SelectListItem
                {
                    Value = e.IdEstado.ToString(),
                    Text = e.NombreEstado
                }).ToList();

                dto.ListaNivelesRiesgo = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Bajo",    Text = "Bajo"    },
                    new SelectListItem { Value = "Medio",   Text = "Medio"   },
                    new SelectListItem { Value = "Alto",    Text = "Alto"    },
                    new SelectListItem { Value = "Crítico", Text = "Crítico" }
                };
            }
            return dto;
        }

        private OdontogramaDto CargarDropdownsOdontograma(OdontogramaDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaPiezas = ctx.PiezasDentales.Where(p => p.IdEstado == 1).Select(p => new SelectListItem
                {
                    Value = p.IdPieza.ToString(),
                    Text = "Pieza " + p.NumeroPieza
                }).ToList();
            }
            return dto;
        }
    }
}