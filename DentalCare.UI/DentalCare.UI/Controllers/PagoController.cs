using DentaCare.LogicaDeNegocio.Pago.ObtenerPago;
using DentaCare.LogicaDeNegocio.Pago.RegistrarPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Catalogo.ObtenerCatalogo;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.ActualizarEstadoPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.ObtenerPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.RegistrarPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using DentalCare.AccesoADatos;
using DentalCare.LogicaDeNegocio.Catalogo.ObtenerCatalogo;
using DentalCare.LogicaDeNegocio.Pagos.ActualizarEstadoPago;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class PagoController : Controller
    {
        private readonly IObtenerPagosLN _obtenerLN;
        private readonly IRegistrarPagoLN _registrarLN;
        private readonly IActualizarEstadoPagoLN _actualizarLN;
        private readonly IObtenerCatalogoLN _catalogoLN;

        public PagoController()
        {
            _obtenerLN = new ObtenerPagosLN();
            _registrarLN = new RegistrarPagoLN();
            _actualizarLN = new ActualizarEstadoPagoLN();
            _catalogoLN = new ObtenerCatalogoLN();
        }

        // GET: Pago/HistorialPagos
        public ActionResult HistorialPagos()
        {
            var lista = _obtenerLN.Obtener();
            return View(lista);
        }

        // GET: Pago/RegistrarPago
        public ActionResult RegistrarPago(int idCita = 0)
        {
            var dto = CargarDropdowns(new PagoDto { IdCitaForm = idCita, IdEstado = 1 });
            return View(dto);
        }

        // POST: Pago/RegistrarPago
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarPago(PagoDto dto)
        {
            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _registrarLN.Registrar(dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "Pago registrado exitosamente.";
            return RedirectToAction("HistorialPagos");
        }

        // GET: Pago/MarcarPendiente/5
        public ActionResult MarcarPendiente(int id)
        {
            var pago = _actualizarLN.ObtenerPorId(id);
            if (pago == null) { TempData["Error"] = "No se encontró el pago."; return RedirectToAction("HistorialPagos"); }
            return View(pago);
        }

        [HttpPost, ActionName("MarcarPendiente")]
        [ValidateAntiForgeryToken]
        public ActionResult MarcarPendienteConfirmado(int id)
        {
            string error = _actualizarLN.RegistrarPendiente(id);
            TempData[error != null ? "Error" : "Exito"] = error ?? "Pago marcado como pendiente.";
            return RedirectToAction("HistorialPagos");
        }

        // GET: Pago/MarcarPagado/5
        public ActionResult MarcarPagado(int id)
        {
            var pago = _actualizarLN.ObtenerPorId(id);
            if (pago == null) { TempData["Error"] = "No se encontró el pago."; return RedirectToAction("HistorialPagos"); }
            return View(pago);
        }

        [HttpPost, ActionName("MarcarPagado")]
        [ValidateAntiForgeryToken]
        public ActionResult MarcarPagadoConfirmado(int id)
        {
            string error = _actualizarLN.ActualizarAPagado(id);
            TempData[error != null ? "Error" : "Exito"] = error ?? "Pago actualizado a pagado.";
            return RedirectToAction("HistorialPagos");
        }

        // ---------------------------------------------------------------
        // Auxiliar: carga dropdowns incluyendo catálogo y pacientes
        // ---------------------------------------------------------------
        private PagoDto CargarDropdowns(PagoDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaMetodosPago = ctx.MetodosPago
                    .Where(m => m.IdEstado == 1)
                    .Select(m => new SelectListItem
                    {
                        Value = m.IdMetodoPago.ToString(),
                        Text = m.Descripcion
                    }).ToList();

                dto.ListaEstados = ctx.Estados
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEstado.ToString(),
                        Text = e.NombreEstado
                    }).ToList();

                // Pacientes (rol Paciente)
                var rolPaciente = ctx.AspNetRoles.FirstOrDefault(r => r.Name == "Paciente");
                if (rolPaciente != null)
                {
                    var idsPacientes = ctx.AspNetUserRoles
                        .Where(ur => ur.RoleId == rolPaciente.Id)
                        .Select(ur => ur.UserId).ToList();

                    dto.ListaPacientes = ctx.Usuarios
                        .Where(u => idsPacientes.Contains(u.ASPNET_USER_ID) && u.IdEstado == 1)
                        .Select(u => new SelectListItem
                        {
                            Value = u.IdUsuario.ToString(),
                            Text = u.Nombre + " " + u.PrimerApellido
                        }).ToList();
                }
                else dto.ListaPacientes = new List<SelectListItem>();

                // Citas finalizadas
                var estadoFin = ctx.Estados.FirstOrDefault(e => e.NombreEstado == "Finalizada");
                if (estadoFin != null)
                {
                    dto.ListaCitas = ctx.Citas
                        .Where(c => c.IdEstado == estadoFin.IdEstado)
                        .Select(c => new SelectListItem
                        {
                            Value = c.IdCita.ToString(),
                            Text = "Cita #" + c.IdCita + " — " + c.Fecha.ToString()
                        }).ToList();
                }
                else dto.ListaCitas = new List<SelectListItem>();
            }

            // Catálogo de tratamientos para el dropdown del paso 2
            dto.ListaCatalogo = _catalogoLN.Obtener()
                .Select(c => new SelectListItem
                {
                    Value = c.IdCatalogo + "|" + c.Costo,
                    Text = c.Nombre + " (₡" + c.Costo.ToString("N0") + ")"
                }).ToList();

            return dto;
        }
    }
}