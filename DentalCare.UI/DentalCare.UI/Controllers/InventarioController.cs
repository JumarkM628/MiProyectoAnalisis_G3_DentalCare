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
using DentalCare.AccesoADatos.Entidades.Producto;
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
        // orden: nombre (por defecto) | utilizacion | compras | lote
        // tratamiento: descripción del tratamiento para mostrar solo los productos usados en él
        public ActionResult InventarioIndex(string orden = "nombre", string tratamiento = null)
        {
            using (var contexto = new Contexto())
            {
                var productos = contexto.Productos.ToList();

                // Nombres de categoría (los IDs de respaldo coinciden con CargarDropdowns)
                var nombresCategoria = new Dictionary<int, string>
                {
                    { 1, "Consumible" }, { 2, "Medicamento" }, { 3, "Instrumental" }, { 4, "Higiene" }
                };
                foreach (var cat in contexto.CategoriasProducto.ToList())
                    nombresCategoria[cat.IdCategoria] = cat.NombreCategoria;

                // Cantidad total utilizada y comprada por producto
                var usoTotal = contexto.UsoProductos
                    .GroupBy(u => u.ID_PRODUCTO)
                    .Select(g => new { Id = g.Key, Total = g.Sum(x => x.CANTIDAD) })
                    .ToList()
                    .ToDictionary(x => x.Id, x => x.Total ?? 0);

                var comprasTotal = contexto.ComprasProducto
                    .GroupBy(c => c.ID_PRODUCTO)
                    .Select(g => new { Id = g.Key, Total = g.Sum(x => x.CANTIDAD) })
                    .ToList()
                    .ToDictionary(x => x.Id, x => x.Total);

                // Filtro por tipo de tratamiento: solo productos usados en ese tratamiento
                var usoMostrado = usoTotal;
                if (!string.IsNullOrWhiteSpace(tratamiento))
                {
                    var idsTratamiento = contexto.PlanesTratamiento
                        .Where(t => t.Descripcion == tratamiento)
                        .Select(t => (int?)t.IdTratamiento)
                        .ToList();

                    usoMostrado = (
                        from u in contexto.UsoProductos
                        join p in contexto.Procedimientos on u.ID_PROCEDIMIENTO equals p.ID_PROCEDIMIENTO
                        where idsTratamiento.Contains(p.ID_TRATAMIENTO)
                        group u by u.ID_PRODUCTO into g
                        select new { Id = g.Key, Total = g.Sum(x => x.CANTIDAD) })
                        .ToList()
                        .ToDictionary(x => x.Id, x => x.Total ?? 0);

                    productos = productos
                        .Where(p => usoMostrado.ContainsKey(p.ID_PRODUCTO))
                        .ToList();
                }

                Func<ProductoEntidad, int> uso = p => usoMostrado.ContainsKey(p.ID_PRODUCTO) ? usoMostrado[p.ID_PRODUCTO] : 0;
                Func<ProductoEntidad, int> compras = p => comprasTotal.ContainsKey(p.ID_PRODUCTO) ? comprasTotal[p.ID_PRODUCTO] : 0;

                switch (orden)
                {
                    case "utilizacion":
                        productos = productos.OrderByDescending(uso).ThenBy(p => p.NOMBRE_PRODUCTO).ToList();
                        break;
                    case "compras":
                        productos = productos.OrderByDescending(compras).ThenBy(p => p.NOMBRE_PRODUCTO).ToList();
                        break;
                    case "lote":
                        // Agrupa por lote y, dentro de cada lote, el más utilizado primero
                        productos = productos.OrderBy(p => p.LOTE).ThenByDescending(uso).ThenBy(p => p.NOMBRE_PRODUCTO).ToList();
                        break;
                    default:
                        orden = "nombre";
                        productos = productos.OrderBy(p => p.NOMBRE_PRODUCTO).ToList();
                        break;
                }

                ViewBag.Orden = orden;
                ViewBag.Tratamiento = tratamiento;
                ViewBag.Tratamientos = contexto.PlanesTratamiento
                    .Where(t => t.Descripcion != null && t.Descripcion != "")
                    .Select(t => t.Descripcion)
                    .Distinct()
                    .OrderBy(d => d)
                    .ToList();
                ViewBag.NombresCategoria = nombresCategoria;
                ViewBag.UsoProducto = usoMostrado;
                ViewBag.ComprasProducto = comprasTotal;

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

                // No se aceptan fechas de vencimiento ya pasadas (salvo que no se haya cambiado la fecha guardada)
                bool fechaSinCambio = producto.FECHA_VENCIMIENTO.HasValue
                    && producto.FECHA_VENCIMIENTO.Value.Date == model.FechaVencimiento.Date;
                if (model.FechaVencimiento.Date < DateTime.Today && !fechaSinCambio)
                {
                    ModelState.AddModelError("FechaVencimiento", "La fecha de vencimiento no puede ser anterior a la fecha actual.");
                    CargarDropdowns();
                    return View(model);
                }

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