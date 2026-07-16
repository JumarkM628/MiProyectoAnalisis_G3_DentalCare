using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DentaCare.LogicaDeNegocio.Reporteria.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto;
using DentaCare.LogicaDeNegocio.Reporteria.Citas;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Citas;
using DentalCare.Abstraccion.Modelo.Reporteria;
using DentalCare.AccesoADatos.Citas.Reporteria;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Reporteria.Producto;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class ReporteriaController : Controller
    {
        private readonly IReporteProductosLN _reporteProductosLN;
        private readonly IReporteLotesLN _reporteLotesLN;
        private readonly IReporteCitasLN _reporteCitasLN;

        public ReporteriaController()
        {
            _reporteProductosLN = new ReporteProductosLN(new ReporteProductosAD(new Contexto()));
            _reporteLotesLN = new ReporteLotesLN(new ReporteLotesAD(new Contexto()));
            _reporteCitasLN = new ReporteCitasLN(new ReporteCitasAD(new Contexto()));
        }

        // GET: Reporteria
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reporteria/ReportesProductos
        public ActionResult ReportesProductos()
        {
            return View();
        }

        // GET: Reporteria/ProductosMasUtilizados
        public ActionResult ProductosMasUtilizados()
        {
            var lista = _reporteProductosLN.ObtenerMasUtilizados();
            return PartialView(lista);
        }

        // GET: Reporteria/ProductosMenosUtilizados
        public ActionResult ProductosMenosUtilizados()
        {
            var lista = _reporteProductosLN.ObtenerMenosUtilizados();
            return PartialView(lista);
        }

        // GET: Reporteria/ProductosMasComprados
        public ActionResult ProductosMasComprados()
        {
            var lista = _reporteProductosLN.ObtenerMasComprados();
            return PartialView(lista);
        }

        // GET: Reporteria/ProductosMenosComprados
        public ActionResult ProductosMenosComprados()
        {
            var lista = _reporteProductosLN.ObtenerMenosComprados();
            return PartialView(lista);
        }

        // GET: Reporteria/HistorialPorTratamiento
        public ActionResult HistorialPorTratamiento()
        {
            var lista = _reporteProductosLN.ObtenerHistorialPorTratamiento();
            return PartialView(lista);
        }

        // GET: Reporteria/LotesMasUtilizados
        public ActionResult LotesMasUtilizados()
        {
            var lista = _reporteLotesLN.ObtenerLotesMasUtilizados();
            return PartialView(lista);
        }

        // GET: Reporteria/LotesMenosUtilizados
        public ActionResult LotesMenosUtilizados()
        {
            var lista = _reporteLotesLN.ObtenerLotesMenosUtilizados();
            return PartialView(lista);
        }

        // GET: Reporteria/LotesMasComprados
        public ActionResult LotesMasComprados()
        {
            var lista = _reporteLotesLN.ObtenerLotesMasComprados();
            return PartialView(lista);
        }

        // GET: Reporteria/LotesMenosComprados
        public ActionResult LotesMenosComprados()
        {
            var lista = _reporteLotesLN.ObtenerLotesMenosComprados();
            return PartialView(lista);
        }

        // GET: Reporteria/HistorialLotePorTratamiento
        public ActionResult HistorialLotePorTratamiento()
        {
            var lista = _reporteLotesLN.ObtenerHistorialLotePorTratamiento();
            return PartialView(lista);
        }

        // GET: Reporteria/ReportesCitas
        public ActionResult ReportesCitas()
        {
            return View();
        }

        // GET: Reporteria/ReportesTratamientos
        public ActionResult ReportesTratamientos()
        {
            // Por ahora reutilizamos una vista existente como marcador de posición
            return View("ReportesProductos");
        }

        // GET: Reporteria/ReportesInventario
        public ActionResult ReportesInventario()
        {
            return View("Inventario");
        }

        // GET: Reporteria/ReportesProductosUtilizados
        public ActionResult ReportesProductosUtilizados()
        {
            return View("ReportesProductos");
        }

        // GET: Reporteria/ReportesPagos
        public ActionResult ReportesPagos()
        {
            return View("Pagos");
        }

        // GET: Reporteria/ReportesPacientesAtendidos
        public ActionResult ReportesPacientesAtendidos()
        {
            return View("Pacientes");
        }

        // GET: Reporteria/ReportesUsuarios
        public ActionResult ReportesUsuarios()
        {
            // Reutilizamos la vista de usuario existente por ahora
            return View("~/Views/Usuario/ObtenerTodosLosUsuarios.cshtml");
        }

        // GET: Reporteria/CitasPorPeriodo?desde=yyyy-MM-dd&hasta=yyyy-MM-dd
        public ActionResult CitasPorPeriodo(DateTime? desde, DateTime? hasta)
        {
            if (!desde.HasValue || !hasta.HasValue)
                return View(new List<CitaReporteDto>());

            var lista = _reporteCitasLN.ObtenerPorPeriodo(desde.Value, hasta.Value);
            return View(lista);
        }


        // GET: Reporteria/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reporteria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reporteria/Create
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

        // GET: Reporteria/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reporteria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Reporteria/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reporteria/Delete/5
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
