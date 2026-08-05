using DentaCare.LogicaDeNegocio.Citas.AgregarCita;
using DentaCare.LogicaDeNegocio.Citas.CambiarEstadoCita;
using DentaCare.LogicaDeNegocio.Citas.ObtenerCitasPaciente;
using DentaCare.LogicaDeNegocio.Citas.ObtenerTodasLasCitas;
using DentaCare.LogicaDeNegocio.UsoProducto.RegistrarUsoProducto;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.AgregarCita;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.CambiarEstadoCita;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerCitaPorId;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerCitasPaciente;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerTodasLasCitas;
using DentalCare.Abstraccion.LogicaDeNegocio.UsoProducto;
using DentalCare.Abstraccion.Modelo.Citas;
using DentalCare.Abstraccion.Modelo.Producto.UsoProducto;
using DentalCare.AccesoADatos;
using DentalCare.LogicaDeNegocio.Citas.ObtenerCitaPorId;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DentalCare.AccesoADatos.UsoProducto.RegistrarUsoProducto;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista,Doctor,Asistente,Paciente")]
    public class CitaController : Controller
    {
        private readonly IObtenerTodasLasCitasLN _obtenerLN;
        private readonly IAgregarCitaLN _agregarLN;
        private readonly ICambiarEstadoCitaLN _cambiarEstadoLN;
        private readonly IRegistrarUsoProductoLN _usoProductoLN;
        private readonly IObtenerCitasPacienteLN _obtenerPacienteLN;
        private readonly IObtenerCitaPorIdLN _obtenerPorIdLN;

        public CitaController()
        {
            _obtenerLN = new ObtenerTodasLasCitasLN();
            _agregarLN = new AgregarCitaLN();
            _cambiarEstadoLN = new CambiarEstadoCitaLN();
            _obtenerPacienteLN = new ObtenerCitasPacienteLN();
            _usoProductoLN = new RegistrarUsoProductoLN(new RegistrarUsoProductoAD(new Contexto()));
            _obtenerPorIdLN = new ObtenerCitaPorIdLN();
        }

        // GET: Cita/ObtenerTodasLasCitas
        public ActionResult ObtenerTodasLasCitas()
        {
            List<CitaDto> lista = _obtenerLN.Obtener();
            return View(lista);
        }

        // GET: Cita/Detalle/5
        public ActionResult Detalle(int id)
        {
            var cita = _obtenerPorIdLN.Obtener(id);
            if (cita == null)
            {
                TempData["Error"] = "No se encontró la cita.";
                return RedirectToAction("ObtenerTodasLasCitas");
            }
            return View("DetalleCita", cita);
        }

        // GET: Cita/AgregarCita
        public ActionResult AgregarCita()
        {
            var dto = CargarDropdowns(new CitaDto());
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarCita(CitaDto dto)
        {
            ModelState.Remove("Hora");
            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _agregarLN.Agregar(dto);
            if (error != null)
            {
                if (error.Contains("no está registrado"))
                    ViewBag.MostrarRegistrarPaciente = true;
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "Cita registrada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        // GET: Confirmación de cancelación
        public ActionResult Cancelar(int id)
        {
            var lista = _obtenerLN.Obtener();
            var cita = lista.FirstOrDefault(c => c.IdCita == id);
            if (cita == null)
            {
                TempData["Error"] = "No se encontró la cita.";
                return RedirectToAction("ObtenerTodasLasCitas");
            }
            return View("CancelarConfirmado", cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelarConfirmado(int id, string motivoCancelacion)
        {
            string error = _cambiarEstadoLN.Cancelar(id, motivoCancelacion);
            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Cancelar", new { id });
            }
            TempData["Exito"] = "Cita cancelada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        // GET: Confirmación de rechazo
        public ActionResult Rechazar(int id)
        {
            var lista = _obtenerLN.Obtener();
            var cita = lista.FirstOrDefault(c => c.IdCita == id);
            if (cita == null)
            {
                TempData["Error"] = "No se encontró la cita.";
                return RedirectToAction("ObtenerTodasLasCitas");
            }
            return View("RechazarConfirmado", cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RechazarConfirmado(int id)
        {
            string error = _cambiarEstadoLN.Rechazar(id);
            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("ObtenerTodasLasCitas");
            }
            TempData["Exito"] = "Cita rechazada. Se notificó al paciente por correo.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            string error = _cambiarEstadoLN.Confirmar(id);
            TempData[error != null ? "Error" : "Exito"] = error ?? "Cita confirmada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        public ActionResult Editar(int id)
        {
            var lista = _obtenerLN.Obtener();
            var cita = lista.FirstOrDefault(c => c.IdCita == id);
            if (cita == null)
            {
                TempData["Error"] = "No se encontró la cita.";
                return RedirectToAction("ObtenerTodasLasCitas");
            }
            return View(CargarDropdowns(cita));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(CitaDto dto)
        {
            ModelState.Remove("Hora");
            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            string nombreEstado = ObtenerNombreEstado(dto.IdEstado);
            if (string.IsNullOrEmpty(nombreEstado))
            {
                TempData["Error"] = "Estado inválido.";
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _cambiarEstadoLN.EditarEstado(dto.IdCita, nombreEstado, 1);
            if (error != null)
            {
                TempData["Error"] = error;
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "La cita fue actualizada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        private string ObtenerNombreEstado(int idEstado)
        {
            using (var ctx = new Contexto())
            {
                var estado = ctx.Estados.FirstOrDefault(e => e.IdEstado == idEstado);
                return estado?.NombreEstado;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Asistir(int id)
        {
            string error = _cambiarEstadoLN.Asistir(id, DateTime.Now.TimeOfDay);
            TempData[error != null ? "Error" : "Exito"] = error ?? "Asistencia registrada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ausente(int id)
        {
            string error = _cambiarEstadoLN.Ausente(id);
            TempData[error != null ? "Error" : "Exito"] = error ?? "Ausencia registrada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finalizar(int id)
        {
            string error = _cambiarEstadoLN.Finalizar(id, DateTime.Now.TimeOfDay);
            TempData[error != null ? "Error" : "Exito"] = error ?? "Cita finalizada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        public ActionResult RegistrarProductos(int id)
        {
            var lista = _obtenerLN.Obtener();
            var cita = lista.FirstOrDefault(c => c.IdCita == id);
            if (cita == null)
            {
                TempData["Error"] = "No se encontró la cita.";
                return RedirectToAction("ObtenerTodasLasCitas");
            }

            ViewBag.IdCita = id;
            ViewBag.NombrePaciente = cita.NombrePaciente;

            var productosUsados = _usoProductoLN.ObtenerProductosUsadosPorCita(id);

            using (var ctx = new Contexto())
            {
                ViewBag.ListaProductos = ctx.Productos
                    .Select(p => new SelectListItem
                    {
                        Value = p.ID_PRODUCTO.ToString(),
                        Text = p.NOMBRE_PRODUCTO
                    }).ToList();
            }

            return View(productosUsados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarProductos(UsoProductoDto dto)
        {
            if (dto == null)
            {
                TempData["Error"] = "Datos inválidos.";
                return RedirectToAction("ObtenerTodasLasCitas");
            }
            if (dto.Cantidad <= 0 || dto.IdProducto == 0)
            {
                TempData["Error"] = "Debe seleccionar un producto y una cantidad válida.";
                return RedirectToAction("RegistrarProductos", new { id = dto.IdCita });
            }

            string error = _usoProductoLN.RegistrarUso(dto);
            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("RegistrarProductos", new { id = dto.IdCita });
            }

            string finalizarError = _cambiarEstadoLN.Finalizar(dto.IdCita, DateTime.Now.TimeOfDay);
            TempData["Exito"] = finalizarError != null
                ? "Producto registrado correctamente. Nota: " + finalizarError
                : "Producto registrado y cita finalizada correctamente.";

            return RedirectToAction("RegistrarProductos", new { id = dto.IdCita });
        }

        // ---------------------------------------------------------------
        // Paciente
        // ---------------------------------------------------------------
        [Authorize(Roles = "Paciente")]
        public ActionResult MisCitas()
        {
            string aspNetUserId = User.Identity.GetUserId();
            var lista = _obtenerPacienteLN.ObtenerPorPaciente(aspNetUserId);
            return View(lista);
        }

        [Authorize(Roles = "Paciente")]
        public ActionResult CancelarMiCita(int id)
        {
            string aspNetUserId = User.Identity.GetUserId();
            var lista = _obtenerPacienteLN.ObtenerPorPaciente(aspNetUserId);
            var cita = lista.FirstOrDefault(c => c.IdCita == id);
            if (cita == null)
            {
                TempData["Error"] = "No se encontró la cita o no te pertenece.";
                return RedirectToAction("MisCitas");
            }
            return View(cita);
        }

        [HttpPost, ActionName("CancelarMiCita")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Paciente")]
        public ActionResult CancelarMiCitaConfirmado(int id)
        {
            string error = _cambiarEstadoLN.Cancelar(id, "Cancelada por el paciente");
            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("MisCitas");
            }
            TempData["Exito"] = "Tu cita fue cancelada correctamente.";
            return RedirectToAction("MisCitas");
        }

        public ActionResult SolicitarCitaUsuario()
        {
            var dto = CargarDropdowns(new CitaDto { IdEstado = 1 });
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SolicitarCitaUsuario(CitaDto dto)
        {
            ModelState.Remove("Hora");
            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _agregarLN.Agregar(dto);
            if (error != null)
            {
                if (error.Contains("no está registrado"))
                    ViewBag.MostrarRegistrarPaciente = true;
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "¡Tu cita fue registrada correctamente! Te esperamos.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        // ---------------------------------------------------------------
        // Auxiliar: carga dropdowns
        // ---------------------------------------------------------------
        private CitaDto CargarDropdowns(CitaDto dto)
        {
            using (var ctx = new Contexto())
            {
                var rolDoctor = ctx.AspNetRoles.FirstOrDefault(r => r.Name == "Doctor");
                if (rolDoctor != null)
                {
                    var idsAspNetDoctores = ctx.AspNetUserRoles
                        .Where(ur => ur.RoleId == rolDoctor.Id)
                        .Select(ur => ur.UserId).ToList();

                    dto.ListaDoctores = ctx.Usuarios
                        .Where(u => idsAspNetDoctores.Contains(u.ASPNET_USER_ID) && u.IdEstado == 1)
                        .Select(u => new SelectListItem
                        {
                            Value = u.IdUsuario.ToString(),
                            Text = u.Nombre + " " + u.PrimerApellido
                        }).ToList();
                }
                else dto.ListaDoctores = new List<SelectListItem>();

                dto.ListaMotivos = ctx.MotivosCita
                    .Where(m => m.IdEstado == 1)
                    .Select(m => new SelectListItem
                    {
                        Value = m.IdMotivo.ToString(),
                        Text = m.Descripcion
                    }).ToList();

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