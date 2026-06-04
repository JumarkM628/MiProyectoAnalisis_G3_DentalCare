using DentaCare.LogicaDeNegocio.Usuarios.EditarUsuario;
using DentaCare.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
using DentaCare.LogicaDeNegocio.Usuarios.ObtenerUsuarioPorId;
using DentaCare.LogicaDeNegocio.Usuarios.RegistrarUsuarios;
using DentalCare.Abstraccion.AccesoADatos.Usuarios.RegistrarUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.EditarUsuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.EliminarUsuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.ObtenerUsuarioPorId;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.RegistrarUsuario;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos;
using DentalCare.LogicaDeNegocio.Usuarios.EliminarUsuario;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class UsuarioController : Controller
    {
        private IObtenerTodosLosUsuariosLN _obtenerTodosLosUsuariosLN;
        private IRegistrarUsuariosLN _registrarUsuariosLN;
        private IObtenerUsuarioPorIdLN _obtenerUsuarioPorIdLN;
        private IEditarUsuarioLN _editarUsuarioLN;
        private readonly IEliminarUsuarioLN _eliminarUsuarioLN;

        public UsuarioController()
        {
            _obtenerTodosLosUsuariosLN = new ObtenerTodosLosUsuariosLN();
            _registrarUsuariosLN = new RegistrarUsuariosLN();
            _obtenerUsuarioPorIdLN = new ObtenerUsuarioPorIdLN();
            _editarUsuarioLN = new EditarUsuarioLN();
            _eliminarUsuarioLN = new EliminarUsuarioLN();
        }

        public ActionResult ObtenerTodosLosUsuarios()
        {
            List<UsuarioDto> listaUsuarios = _obtenerTodosLosUsuariosLN.Obtener();
            return View(listaUsuarios);
        }

        public ActionResult DetallesDelUsuario(int id)
        {
            UsuarioDto elUsuario = _obtenerUsuarioPorIdLN.Obtener(id);
            return View(elUsuario);
        }

        public ActionResult RegistrarUsuario()
        {
            var dto = CargarDropdowns(new UsuarioDto());
            return View(dto);
        }

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

            // 🔹 Cargar el rol actual del AspNetUser
            using (var ctx = new Contexto())
            {
                var userRole = ctx.AspNetUserRoles
                    .FirstOrDefault(ur => ur.UserId == dto.AspNetUserId);
                if (userRole != null)
                {
                    dto.RoleId = userRole.RoleId;
                    // Opcional: también cargar el nombre del rol para mostrarlo
                    var role = ctx.AspNetRoles.FirstOrDefault(r => r.Id == userRole.RoleId);
                    dto.RoleName = role?.Name;
                }
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

        public ActionResult EliminarUsuario(int id)
        {
            UsuarioDto dto = _obtenerUsuarioPorIdLN.Obtener(id);
            if (dto == null)
            {
                TempData["Error"] = "No se encontró el usuario.";
                return RedirectToAction("ObtenerTodosLosUsuarios");
            }
            return View(dto);
        }

        [HttpPost]
        public ActionResult EliminarUsuario(int id, FormCollection collection)
        {
            string aspNetUserIdAdmin = User.Identity.GetUserId();

            string error = _eliminarUsuarioLN.Eliminar(id, aspNetUserIdAdmin);
            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("ObtenerTodosLosUsuarios");
            }

            TempData["Exito"] = "Usuario eliminado correctamente.";
            return RedirectToAction("ObtenerTodosLosUsuarios");
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

                dto.ListaRoles = ctx.AspNetRoles
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id,
                        Text = r.Name
                    }).ToList();
            }
            return dto;
        }
    }
}