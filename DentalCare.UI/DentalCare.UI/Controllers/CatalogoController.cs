using DentalCare.Abstraccion.LogicaDeNegocio.Catalogo.ActualizarCosto;
using DentalCare.Abstraccion.LogicaDeNegocio.Catalogo.ObtenerCatalogo;
using DentalCare.Abstraccion.Modelo.Catalogo;
using DentalCare.AccesoADatos;
using DentalCare.LogicaDeNegocio.Catalogo.ActualizarCosto;
using DentalCare.LogicaDeNegocio.Catalogo.ObtenerCatalogo;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class CatalogoController : Controller
    {
        private readonly IObtenerCatalogoLN _obtenerLN;
        private readonly IActualizarCostoLN _actualizarLN;

        public CatalogoController()
        {
            _obtenerLN = new ObtenerCatalogoLN();
            _actualizarLN = new ActualizarCostoLN();
        }

        // GET: Catalogo/CostosTratamiento
        public ActionResult CostosTratamiento()
        {
            var lista = _obtenerLN.Obtener();
            return View(lista);
        }

        // GET: Catalogo/EditarCosto/5
        public ActionResult EditarCosto(int id)
        {
            var dto = _obtenerLN.ObtenerPorId(id);
            if (dto == null)
            {
                TempData["Error"] = "No se encontró el tratamiento.";
                return RedirectToAction("CostosTratamiento");
            }
            CargarDropdowns(dto);
            return View(dto);
        }

        // POST: Catalogo/EditarCosto/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCosto(int id, CatalogoDto dto)
        {
            if (!ModelState.IsValid)
            {
                // Recargar datos de solo lectura
                var original = _obtenerLN.ObtenerPorId(id);
                if (original != null)
                {
                    dto.Nombre = original.Nombre;
                    dto.Categoria = original.Categoria;
                    dto.Costo = original.Costo;
                }
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _actualizarLN.Actualizar(id, dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            TempData["Exito"] = "Costo actualizado correctamente.";
            return RedirectToAction("CostosTratamiento");
        }

        private void CargarDropdowns(CatalogoDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaEstados = ctx.Estados
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEstado.ToString(),
                        Text = e.NombreEstado
                    }).ToList();
            }
        }
    }
}