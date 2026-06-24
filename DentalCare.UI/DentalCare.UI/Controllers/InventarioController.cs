using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DentaCare.LogicaDeNegocio.Producto.EditarProducto;
using DentalCare.Abstraccion.LogicaDeNegocio.Producto;
using DentalCare.Abstraccion.Modelo.Producto;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Producto.EditarProducto;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista,Doctor,Asistente")]
    public class InventarioController : Controller
    {
        private readonly IEditarProductoLN _editarProductoLN;

        public InventarioController()
        {
            _editarProductoLN = new EditarProductoLN(new EditarProductoAD(new Contexto()));
        }

        // GET: Inventario
        public ActionResult InventarioIndex()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        [Authorize(Roles = "Doctor,Admin")]
        public ActionResult Edit(int id)
        {
            var producto = _editarProductoLN.ObtenerProductoPorId(id);
            if (producto == null)
                return HttpNotFound();

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public ActionResult Edit(ProductoDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string nombreUsuario = User.Identity.Name;

            string error = _editarProductoLN.EditarProducto(model, nombreUsuario);

            if (error != null)
            {
                ModelState.AddModelError("", error);
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
