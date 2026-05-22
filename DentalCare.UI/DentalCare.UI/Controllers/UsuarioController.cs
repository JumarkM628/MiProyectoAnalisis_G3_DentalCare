using DentaCare.LogicaDeNegocio.Usuarios.EditarUsuario;
using DentaCare.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
using DentaCare.LogicaDeNegocio.Usuarios.ObtenerUsuarioPorId;
using DentaCare.LogicaDeNegocio.Usuarios.RegistrarUsuarios;
using DentalCare.Abstraccion.AccesoADatos.Usuarios.RegistrarUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.EditarUsuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.ObtenerUsuarioPorId;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.RegistrarUsuario;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private IObtenerTodosLosUsuariosLN _obtenerTodosLosUsuariosLN;
        private IRegistrarUsuariosLN _registrarUsuariosLN;
        private IObtenerUsuarioPorIdLN _obtenerUsuarioPorIdLN;
        private IEditarUsuarioLN _editarUsuarioLN;
        public UsuarioController()
        {
            _obtenerTodosLosUsuariosLN = new ObtenerTodosLosUsuariosLN();
            _registrarUsuariosLN = new RegistrarUsuariosLN();
            _obtenerUsuarioPorIdLN = new ObtenerUsuarioPorIdLN();
            _editarUsuarioLN = new EditarUsuarioLN();
        }

        // GET: Usuario
        public ActionResult ObtenerTodosLosUsuarios()
        {
            List<UsuarioDto> listaUsuarios = _obtenerTodosLosUsuariosLN.Obtener();
            return View(listaUsuarios);
        }

        // GET: Usuario/Details/5
        public ActionResult DetallesDelUsuario(int id)
        {
            UsuarioDto elUsuario = _obtenerUsuarioPorIdLN.Obtener(id);
            return View(elUsuario);
        }

        // GET: Usuario/Create
        public ActionResult RegistrarUsuario()
        {
            var dto = CargarDropdowns(new UsuarioDto());
            return View(dto);
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarUsuario(UsuarioDto dto)
        {
            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _registrarUsuariosLN.RegistrarUsuario(dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "Usuario creado correctamente.";
            return RedirectToAction("ObtenerTodosLosUsuarios");
        }

        // GET: Usuario/Edit/5
        public ActionResult EditarUsuario(int id)
        {
            UsuarioDto dto = _obtenerUsuarioPorIdLN.Obtener(id);

            if (dto == null)
            {
                TempData["Error"] = "No se encontró el usuario.";
                return RedirectToAction("ObtenerTodosLosUsuarios");
            }

            CargarDropdowns(dto);
            return View(dto);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult EditarUsuario(int id, UsuarioDto dto)
        {
            dto.IdUsuario = id;

            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _editarUsuarioLN.Editar(dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "Usuario actualizado correctamente.";
            return RedirectToAction("ObtenerTodosLosUsuarios");
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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

        private UsuarioDto CargarDropdowns(UsuarioDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaAreas = ctx.Areas
                    .Where(a => a.IdEstado == 1)
                    .Select(a => new SelectListItem
                    {
                        Value = a.IdAreaUsuario.ToString(),
                        Text = a.NombreTipoUsuario
                    }).ToList();

                dto.ListaEspecialidades = ctx.Especialidades
                    .Where(e => e.IdEstado == 1)
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEspecialidad.ToString(),
                        Text = e.NombreEspecialidad
                    }).ToList();

                dto.ListaEstados = ctx.Estados
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEstado.ToString(),
                        Text = e.NombreEstado
                    }).ToList();

                dto.ListaTiposCedula = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Física",   Text = "Física"   },
                    new SelectListItem { Value = "Jurídica", Text = "Jurídica"  },
                    new SelectListItem { Value = "DIMEX",    Text = "DIMEX"     },
                    new SelectListItem { Value = "NITE",     Text = "NITE"      }
                };

                // Solo AspNetUsers sin perfil clínico asignado aún
                var idsVinculados = ctx.Usuarios
                    .Select(u => u.ASPNET_USER_ID)
                    .ToList();

                dto.ListaAspNetUsuarios = ctx.AspNetUsers
                    .Where(u => !idsVinculados.Contains(u.Id))
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id,
                        Text = u.UserName + " (" + u.Email + ")"
                    }).ToList();
            }

            return dto;
        }
    }
}
