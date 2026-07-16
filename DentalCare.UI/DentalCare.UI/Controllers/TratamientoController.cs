using DentalCare.Abstraccion.LogicaDeNegocio.Tratamiento.ObtenerTratamientoLN;
using DentalCare.Abstraccion.LogicaDeNegocio.Tratamiento.RegistrarTratamiento;
using DentalCare.Abstraccion.Modelo.Tratamientos;
using DentalCare.AccesoADatos;
using DentalCare.LogicaDeNegocio.Tratamientos.ObtenerTratamientosPorCita;
using DentalCare.LogicaDeNegocio.Tratamientos.RegistrarTratamiento;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista,Doctor")]
    public class TratamientoController : Controller
    {
        private readonly IRegistrarTratamientoLN _registrarLN;
        private readonly IObtenerTratamientosPorCitaLN _obtenerLN;

        public TratamientoController()
        {
            _registrarLN = new RegistrarTratamientoLN();
            _obtenerLN = new ObtenerTratamientosPorCitaLN();
        }

        // ---------------------------------------------------------------
        // GET: Tratamiento/ResumenCita/5
        // Escenario 5: muestra todos los tratamientos de una cita con total
        // ---------------------------------------------------------------
        public ActionResult ResumenCita(int id)
        {
            List<TratamientoDto> lista = _obtenerLN.Obtener(id);
            ViewBag.IdCita = id;
            return View(lista);
        }

        // ---------------------------------------------------------------
        // GET: Tratamiento/RegistrarTratamiento?idCita=X
        // Escenario 1 y 3: formulario para agregar tratamiento a una cita
        // ---------------------------------------------------------------
        public ActionResult RegistrarTratamiento(int idCita = 0)
        {
            var dto = CargarDropdowns(new TratamientoDto { IdCitaForm = idCita, IdEstado = 1 });
            return View(dto);
        }

        // POST: Tratamiento/RegistrarTratamiento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarTratamiento(TratamientoDto dto)
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

            TempData["Exito"] = "Tratamiento registrado correctamente.";

            // Escenario 3: redirigir al resumen para ver acumulado y agregar más
            return RedirectToAction("ResumenCita", new { id = dto.IdCitaForm });
        }

        // ---------------------------------------------------------------
        // Auxiliar: carga dropdowns
        // ---------------------------------------------------------------
        private TratamientoDto CargarDropdowns(TratamientoDto dto)
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
            return dto;
        }
    }
}