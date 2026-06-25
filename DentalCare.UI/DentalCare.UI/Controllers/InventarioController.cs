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
using Microsoft.AspNet.Identity;
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
            using (var contexto = new Contexto())
            {
                var productos = contexto.Productos.ToList();
                return View(productos);
            }
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

            // Obtener usuario logueado (ASP.NET Identity)
            using (var ctx = new Contexto())
            {
                string aspNetUserId = User.Identity.GetUserId();
                var usuarioActual = ctx.Usuarios.FirstOrDefault(u => u.ASPNET_USER_ID == aspNetUserId);

                if (usuarioActual != null)
                {
                    model.RegistradoPor = usuarioActual.IdUsuario;
                }
            }

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

        // =========================
        // EDITAR PRODUCTO
        // =========================

        [Authorize(Roles = "Doctor,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("InventarioIndex");

            using (var contexto = new Contexto())
            {
                var producto = contexto.Productos.Find(id);

                if (producto == null)
                    return HttpNotFound();

                var dto = new ProductoDto
                {
                    IdProducto = producto.ID_PRODUCTO,
                    CodigoProducto = producto.CODIGO_PRODUCTO,
                    NombreProducto = producto.NOMBRE_PRODUCTO,
                    Descripcion = producto.DESCRIPCION,
                    IdCategoria = producto.ID_CATEGORIA,
                    UnidadMedida = producto.UNIDAD_MEDIDA,
                    CantidadActual = producto.STOCK_ACTUAL ?? 0,
                    CantidadMinima = producto.STOCK_MINIMO ?? 0,
                    Lote = producto.LOTE,
                    FechaVencimiento = producto.FECHA_VENCIMIENTO ?? DateTime.Now,
                    IdProveedor = contexto.ProveedorProductos
                        .Where(x => x.IdProducto == producto.ID_PRODUCTO)
                        .Select(x => x.IdProveedor)
                        .FirstOrDefault()
                };

                CargarDropdowns();
                return View(dto);
            }
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

            using (var contexto = new Contexto())
            {
                var producto = contexto.Productos.Find(model.IdProducto);

                if (producto == null)
                    return HttpNotFound();

                producto.CODIGO_PRODUCTO = model.CodigoProducto;
                producto.NOMBRE_PRODUCTO = model.NombreProducto;
                producto.DESCRIPCION = model.Descripcion;
                producto.ID_CATEGORIA = model.IdCategoria;
                producto.UNIDAD_MEDIDA = model.UnidadMedida;
                producto.STOCK_ACTUAL = model.CantidadActual;
                producto.STOCK_MINIMO = model.CantidadMinima;
                producto.LOTE = model.Lote;
                producto.FECHA_VENCIMIENTO = model.FechaVencimiento;

                var rel = contexto.ProveedorProductos
                    .FirstOrDefault(x => x.IdProducto == model.IdProducto);

                if (rel != null)
                {
                    rel.IdProveedor = model.IdProveedor;
                }

                contexto.SaveChanges();
            }

            TempData["Exito"] = "El inventario fue actualizado correctamente.";
            return RedirectToAction("InventarioIndex");
        }

        // =========================
        // DELETE (sin implementar aún)
        // =========================

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("InventarioIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}