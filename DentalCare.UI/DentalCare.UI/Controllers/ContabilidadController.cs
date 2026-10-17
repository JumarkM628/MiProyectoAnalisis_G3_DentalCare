using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DentalCare.AccesoADatos;
using DentalCare.UI.Models.Contabilidad;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class ContabilidadController : Controller
    {
        // GET: Contabilidad/Menu
        public ActionResult Menu()
        {
            return View();
        }

        // GET: Contabilidad/RegistroPago
        public ActionResult RegistroPago()
        {
            return View();
        }

        // POST: Contabilidad/RegistroPago
        [HttpPost]
        public ActionResult RegistroPago(FormCollection collection)
        {
            try
            {
                // TODO: Agregar lógica de registro de pago (MDC-001, MDC-007)
                return RedirectToAction("Menu");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contabilidad/CostosTratamiento
        public ActionResult CostosTratamiento()
        {
            return View();
        }

        // POST: Contabilidad/CostosTratamiento
        [HttpPost]
        public ActionResult CostosTratamiento(int? idCita, DateTime? desde, DateTime? hasta)
        {
            var vm = new CostosTratamientoViewModel { IdCita = idCita, Desde = desde, Hasta = hasta };

            if (desde.HasValue && hasta.HasValue && desde.Value.Date > hasta.Value.Date)
            {
                vm.Error = "La fecha \"Hasta\" no puede ser anterior a la fecha \"Desde\".";
                return View(vm);
            }

            using (var ctx = new Contexto())
            {
                var q = ctx.PlanesTratamiento.AsQueryable();
                if (idCita.HasValue) q = q.Where(t => t.IdCita == idCita.Value);
                if (desde.HasValue)
                {
                    var d = desde.Value.Date;
                    q = q.Where(t => t.FechaInicio >= d);
                }
                if (hasta.HasValue)
                {
                    var h = hasta.Value.Date.AddDays(1); 
                    q = q.Where(t => t.FechaInicio < h);
                }

                vm.Items = (from t in q
                            join e in ctx.Estados on t.IdEstado equals e.IdEstado into eg
                            from e in eg.DefaultIfEmpty()
                            orderby t.FechaInicio
                            select new CostoTratamientoItem
                            {
                                IdPlan = t.IdTratamiento, 
                                Descripcion = t.Descripcion,
                                FechaInicio = t.FechaInicio,
                                FechaFin = t.FechaFin,
                                Estado = e != null ? e.NombreEstado : "-",
                                IdCita = t.IdCita,
                                Monto = t.Monto
                            }).ToList();
            }
            return View(vm);
        }

        // GET: Contabilidad/Gastos
        public ActionResult Gastos()
        {
            return View();
        }

        // POST: Contabilidad/Gastos
        [HttpPost]
        public ActionResult Gastos(FormCollection collection)
        {
            try
            {
                // TODO: Agregar lógica de registro de gastos (MDC-003, MDC-006)
                return RedirectToAction("Menu");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contabilidad/EstadoPagos
        public ActionResult EstadoPagos()
        {
            return View();
        }

        // POST: Contabilidad/EstadoPagos
        [HttpPost]
        public ActionResult EstadoPagos(FormCollection collection)
        {
            try
            {
                // TODO: Agregar lógica de estado de pagos (MDC-004, MDC-005)
                return RedirectToAction("Menu");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contabilidad/Anticipo
        public ActionResult Anticipo()
        {
            return View();
        }

        // POST: Contabilidad/Anticipo
        [HttpPost]
        public ActionResult Anticipo(FormCollection collection)
        {
            try
            {
                // TODO: Agregar lógica de anticipos (MDC-008)
                return RedirectToAction("Menu");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contabilidad/EditarPago
        public ActionResult EditarPago()
        {
            return View();
        }

        // POST: Contabilidad/EditarPago
        [HttpPost]
        public ActionResult EditarPago(FormCollection collection)
        {
            try
            {
                // TODO: Agregar lógica de edición de pago (MDC-009)
                return RedirectToAction("Menu");
            }
            catch
            {
                return View();
            }
        }
    }
}
