using DentaCare.LogicaDeNegocio.Citas.AgregarCita;
using DentaCare.LogicaDeNegocio.Citas.CambiarEstadoCita;
using DentaCare.LogicaDeNegocio.Citas.ObtenerTodasLasCitas;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.AgregarCita;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.CambiarEstadoCita;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerTodasLasCitas;
using DentalCare.Abstraccion.Modelo.Citas;
using DentalCare.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista,Doctor,Asistente,Paciente")]
    public class CitaController : Controller
    {
        private readonly IObtenerTodasLasCitasLN _obtenerLN;
        private readonly IAgregarCitaLN _agregarLN;
        private readonly ICambiarEstadoCitaLN _cambiarEstadoLN;

        public CitaController()
        {
            _obtenerLN = new ObtenerTodasLasCitasLN();
            _agregarLN = new AgregarCitaLN();
            _cambiarEstadoLN = new CambiarEstadoCitaLN();
        }

        // GET: Cita/ObtenerTodasLasCitas
        public ActionResult ObtenerTodasLasCitas()
        {
            List<CitaDto> lista = _obtenerLN.Obtener();
            return View(lista);
        }

        // GET: Cita/Create
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
            // Usar la vista existente "CancelarConfirmado"
            return View("CancelarConfirmado", cita);
        }

        // POST: Procesar cancelación (sin ActionName)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelarConfirmado(int id)
        {
            string error = _cambiarEstadoLN.Cancelar(id);
            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("ObtenerTodasLasCitas");
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
            // Usar la vista existente "RechazarConfirmado"
            return View("RechazarConfirmado", cita);
        }

        // POST: Procesar rechazo (sin ActionName)
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

        // POST: Confirmar (ya funcionaba)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            string error = _cambiarEstadoLN.Confirmar(id);
            TempData[error != null ? "Error" : "Exito"] =
                error ?? "Cita confirmada correctamente.";
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
            // Cargar dropdowns con los datos de la cita
            var dto = CargarDropdowns(cita);
            return View(dto);
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

            // Obtener el nombre del estado a partir del IdEstado seleccionado
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

        // Método auxiliar para obtener nombre de estado por ID
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
            TimeSpan horaInicio = DateTime.Now.TimeOfDay;
            string error = _cambiarEstadoLN.Asistir(id, horaInicio);
            TempData[error != null ? "Error" : "Exito"] =
                error ?? "Asistencia registrada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        // POST: Ausente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ausente(int id)
        {
            string error = _cambiarEstadoLN.Ausente(id);
            TempData[error != null ? "Error" : "Exito"] =
                error ?? "Ausencia registrada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }

        // POST: Finalizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finalizar(int id)
        {
            TimeSpan horaFin = DateTime.Now.TimeOfDay;
            string error = _cambiarEstadoLN.Finalizar(id, horaFin);
            TempData[error != null ? "Error" : "Exito"] =
                error ?? "Cita finalizada correctamente.";
            return RedirectToAction("ObtenerTodasLasCitas");
        }



        // ---------------------------------------------------------------
        // Auxiliar: carga dropdowns
        // ---------------------------------------------------------------
        private CitaDto CargarDropdowns(CitaDto dto)
        {
            using (var ctx = new Contexto())
            {
                // Solo doctores (rol Doctor)
                var rolDoctor = ctx.AspNetRoles
                    .FirstOrDefault(r => r.Name == "Doctor");

                if (rolDoctor != null)
                {
                    var idsAspNetDoctores = ctx.AspNetUserRoles
                        .Where(ur => ur.RoleId == rolDoctor.Id)
                        .Select(ur => ur.UserId)
                        .ToList();

                    dto.ListaDoctores = ctx.Usuarios
                        .Where(u => idsAspNetDoctores.Contains(u.ASPNET_USER_ID)
                                 && u.IdEstado == 1)
                        .Select(u => new SelectListItem
                        {
                            Value = u.IdUsuario.ToString(),
                            Text = u.Nombre + " " + u.PrimerApellido
                        }).ToList();
                }
                else
                {
                    dto.ListaDoctores = new List<SelectListItem>();
                }

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
