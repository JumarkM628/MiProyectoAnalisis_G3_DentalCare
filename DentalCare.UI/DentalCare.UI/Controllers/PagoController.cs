using DentaCare.LogicaDeNegocio.Pago.ObtenerPago;
using DentaCare.LogicaDeNegocio.Pago.RegistrarPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.ObtenerPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.RegistrarPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using DentalCare.AccesoADatos;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class PagoController : Controller
    {
        private readonly IObtenerPagosLN _obtenerLN;
        private readonly IRegistrarPagoLN _registrarLN;

        public PagoController()
        {
            _obtenerLN = new ObtenerPagosLN();
            _registrarLN = new RegistrarPagoLN();
        }

        // GET: Pago/HistorialPagos — Escenario 5
        public ActionResult HistorialPagos()
        {
            List<PagoDto> lista = _obtenerLN.Obtener();
            return View(lista);
        }

        // GET: Pago/RegistrarPago?idCita=X
        public ActionResult RegistrarPago(int idCita = 0)
        {
            var dto = CargarDropdowns(new PagoDto { IdCitaForm = idCita, IdEstado = 1 });
            return View(dto);
        }

        // POST: Pago/RegistrarPago
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarPago(PagoDto dto)
        {
            // Escenario 2: validar campos obligatorios
            if (!ModelState.IsValid)
            {
                CargarDropdowns(dto);
                return View(dto);
            }

            // Escenario 1 y 3: registrar pago con método seleccionado
            string error = _registrarLN.Registrar(dto);
            if (error != null)
            {
                ModelState.AddModelError(string.Empty, error);
                CargarDropdowns(dto);
                return View(dto);
            }

            // Escenario 4: mensaje de confirmación
            TempData["Exito"] = "Pago registrado exitosamente.";
            return RedirectToAction("HistorialPagos");
        }

        // ---------------------------------------------------------------
        // Auxiliar: carga dropdowns
        // ---------------------------------------------------------------
        private PagoDto CargarDropdowns(PagoDto dto)
        {
            using (var ctx = new Contexto())
            {
                dto.ListaMetodosPago = ctx.MetodosPago
                    .Where(m => m.IdEstado == 1)
                    .Select(m => new SelectListItem
                    {
                        Value = m.IdMetodoPago.ToString(),
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