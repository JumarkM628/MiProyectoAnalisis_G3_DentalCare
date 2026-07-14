using DentalCare.Abstraccion.LogicaDeNegocio.Gastos;
using DentalCare.Abstraccion.LogicaDeNegocio.Gastos.ObtenerGastos;
using DentalCare.Abstraccion.Modelo.Gasto;
using DentalCare.AccesoADatos;
using DentalCare.LogicaDeNegocio.Gastos.ObtenerGastos;
using DentalCare.LogicaDeNegocio.Gastos.RegistrarGasto;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    // Escenario 3 MDC-004: solo Admin puede gestionar gastos
    [Authorize(Roles = "Admin")]
    public class GastoController : Controller
    {
        private readonly IObtenerGastosLN _obtenerLN;
        private readonly IRegistrarGastoLN _registrarLN;

        public GastoController()
        {
            _obtenerLN = new ObtenerGastosLN();
            _registrarLN = new RegistrarGastoLN();
        }

        // GET: Gasto/ReporteGastos — Escenario 5
        public ActionResult ReporteGastos()
        {
            List<GastoDto> lista = _obtenerLN.Obtener();
            return View(lista);
        }

        // GET: Gasto/RegistrarGasto
        public ActionResult RegistrarGasto()
        {
            var dto = CargarDropdowns(new GastoDto());
            return View(dto);
        }

        // POST: Gasto/RegistrarGasto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarGasto(GastoDto dto)
        {
            // Escenario 2: validar campos obligatorios
            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            string error = _registrarLN.Registrar(dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            // Escenario 4: mensaje de confirmación
            TempData["Exito"] = "Gasto registrado exitosamente.";
            return RedirectToAction("ReporteGastos");
        }

        private GastoDto CargarDropdowns(GastoDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaEstados = ctx.Estados
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEstado.ToString(),
                        Text = e.NombreEstado
                    }).ToList();

                // Escenario 3: categorías fijas de tipo de gasto
                dto.ListaTiposGasto = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Servicios",      Text = "Servicios"      },
                    new SelectListItem { Value = "Insumos",        Text = "Insumos"        },
                    new SelectListItem { Value = "Mantenimiento",  Text = "Mantenimiento"  },
                    new SelectListItem { Value = "Equipos",        Text = "Equipos"        },
                    new SelectListItem { Value = "Salarios",       Text = "Salarios"       },
                    new SelectListItem { Value = "Administrativo", Text = "Administrativo" },
                    new SelectListItem { Value = "Otros",          Text = "Otros"          }
                };
            }
            return dto;
        }
    }
}