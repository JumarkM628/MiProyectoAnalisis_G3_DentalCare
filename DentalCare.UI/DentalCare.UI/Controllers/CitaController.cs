using DentaCare.LogicaDeNegocio.Citas.AgregarCita;
using DentaCare.LogicaDeNegocio.Citas.ObtenerTodasLasCitas;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.AgregarCita;
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

        public CitaController()
        {
            _obtenerLN = new ObtenerTodasLasCitasLN();
            _agregarLN = new AgregarCitaLN();
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

        // POST: Cita/Create
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
                // Escenario 4: ofrecer opción de registrar paciente
                if (error.Contains("no está registrado"))
                    ViewBag.MostrarRegistrarPaciente = true;

                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "Cita registrada correctamente.";
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
