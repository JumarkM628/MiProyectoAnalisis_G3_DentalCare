using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DentaCare.LogicaDeNegocio.Producto.EditarProducto;
using DentaCare.LogicaDeNegocio.Producto.RegistrarProducto;
using DentalCare.Abstraccion.LogicaDeNegocio.Producto;
using DentalCare.Abstraccion.Modelo.Producto;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Producto.EditarProducto;
using DentalCare.AccesoADatos.Producto.RegistrarProducto;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista,Doctor,Asistente")]
    public class InventarioController : Controller
    {
        private readonly IEditarProductoLN _editarProductoLN;
        private readonly IRegistrarProductoLN _registrarProductoLN;

        public InventarioController()
        {
            var contexto = new Contexto();
            _editarProductoLN = new EditarProductoLN(new EditarProductoAD(contexto));
            _registrarProductoLN = new RegistrarProductoLN(new RegistrarProductoAD(contexto));
        }

        // GET: Inventario
        public ActionResult InventarioIndex()
        {
            var productos = new List<ProductoDto>(); //
            return View(productos);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            CargarDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public ActionResult Create(ProductoDto model)
        {
            if (!ModelState.IsValid)
            {
                CargarDropdowns();
                return View(model);
            }

            string nombreUsuario = User.Identity.Name;

            string error = _registrarProductoLN.RegistrarProducto(model);

            if (error != null)
            {
                ModelState.AddModelError("", error);
                CargarDropdowns();
                return View(model);
            }

            TempData["Exito"] = "El producto fue registrado correctamente en el inventario.";
            return RedirectToAction("InventarioIndex");
        }

        private void CargarDropdowns()
        {
            var categorias = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Consumible" },
                new SelectListItem { Value = "2", Text = "Medicamento" },
                new SelectListItem { Value = "3", Text = "Instrumental" },
                new SelectListItem { Value = "4", Text = "Higiene" }
            };

            var proveedores = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "MedSupply CR" },
                new SelectListItem { Value = "2", Text = "DentaLab" },
                new SelectListItem { Value = "3", Text = "RadioChem" }
            };

            ViewBag.Categorias = categorias;
            ViewBag.Proveedores = proveedores;
        }

        [Authorize(Roles = "Doctor,Admin")]
        public ActionResult Edit(int id)
        {
            var producto = _editarProductoLN.ObtenerProductoPorId(id);
            if (producto == null)
                return HttpNotFound();

            CargarDropdowns();
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public ActionResult Edit(ProductoDto model)
        {
            if (!ModelState.IsValid)
            {
                CargarDropdowns();
                return View(model);
            }

            string nombreUsuario = User.Identity.Name;

            string error = _editarProductoLN.EditarProducto(model, nombreUsuario);

            if (error != null)
            {
                ModelState.AddModelError("", error);
                CargarDropdowns();
                return View(model);
            }

            TempData["Exito"] = "El inventario fue actualizado correctamente.";
            return RedirectToAction("InventarioIndex");
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
