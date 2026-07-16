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

        // GET: Reporteria/ReportesTratamientos?desde=yyyy-MM-dd&hasta=yyyy-MM-dd
        public ActionResult ReportesTratamientos(DateTime? desde, DateTime? hasta)
        {
            using (var contexto = new Contexto())
            {
                var query = contexto.PlanesTratamiento.AsQueryable();

                if (desde.HasValue)
                    query = query.Where(t => t.FechaInicio >= desde.Value);

                if (hasta.HasValue)
                    query = query.Where(t => t.FechaInicio <= hasta.Value);

                var lista = query.OrderBy(t => t.FechaInicio).ToList();

                // Generar página completa (usa mismo layout/estilos del proyecto) para evitar crear archivos de vista
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang=\"es\">\n<head>\n    <meta charset=\"utf-8\" />\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>Reportes de Tratamientos - Clínica Dental Dra. Rebeca</title>\n    <link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" />\n    <link href=\"https://fonts.googleapis.com/css2?family=Playfair+Display:wght@400;600;700&family=DM+Sans:wght@300;400;500;600&display=swap\" rel=\"stylesheet\" />\n</head>\n<body>");

                // Navbar (copiado del layout)
                sb.AppendLine("<nav class=\"main-nav\">\n  <div class=\"nav-inner\">\n    <div class=\"navbar-title\">Clínica dental y especialidades Dra. Rebeca</div>\n    <div class=\"nav-links-wrapper\" id=\"mainNavLinks\">\n      <ul class=\"nav-links\">\n        <li><a class=\"nav-link\" href=\"/\">Inicio</a></li>\n        <li><a class=\"nav-link\" href=\"/Reporteria\">Reporteria</a></li>\n      </ul>\n    </div>\n  </div>\n</nav>");

                // Contenido
                sb.AppendLine("<div class=\"page-wrapper\">\n<div class=\"usuarios-page\">\n  <div class=\"usuarios-header\">\n    <div>\n      <h1 class=\"usuarios-titulo\"><i class=\"fa fa-tooth\"></i> Reportes de Tratamientos</h1>\n      <p class=\"usuarios-subtitulo\">Ver el historial de tratamientos y filtrarlo por fecha.</p>\n    </div>\n    <div>\n      <a href=\"/Reporteria\" class=\"btn-usuario-primary\">← Volver a Reportería</a>\n    </div>\n  </div>\n  <section class=\"usuarios-table-card rep-contenido\">\n    <form method=\"get\" action=\"/Reporteria/ReportesTratamientos\" class=\"form-inline\">\n      <label>Desde: <input type=\"date\" name=\"desde\" /></label>\n      <label style=\"margin-left:12px;\">Hasta: <input type=\"date\" name=\"hasta\" /></label>\n      <button type=\"submit\" class=\"btn-usuario-primary\" style=\"margin-left:12px;\">Filtrar</button>\n    </form>");

                if (lista == null || !lista.Any())
                {
                    sb.AppendLine("<p class=\"rep-placeholder\" style=\"margin-top:20px;\">No hay tratamientos para mostrar.</p>");
                }
                else
                {
                    sb.AppendLine("<table class=\"table\" style=\"margin-top:16px; width:100%; border-collapse:collapse;\"><thead><tr><th>ID_TRATAMIENTO</th><th>DESCRIPCION</th><th>FECHA_INICIO</th><th>FECHA_FIN</th><th>ID_ESTADO</th><th>MONTO</th><th>ID_CITA</th></tr></thead><tbody>");
                    foreach (var t in lista)
                    {
                        sb.AppendLine($"<tr><td>{t.IdTratamiento}</td><td>{System.Net.WebUtility.HtmlEncode(t.Descripcion)}</td><td>{(t.FechaInicio.HasValue? t.FechaInicio.Value.ToString("yyyy-MM-dd") : "")}</td><td>{(t.FechaFin.HasValue? t.FechaFin.Value.ToString("yyyy-MM-dd") : "")}</td><td>{t.IdEstado}</td><td>{(t.Monto.HasValue? t.Monto.Value.ToString("F2") : "")}</td><td>{(t.IdCita.HasValue? t.IdCita.ToString() : "")}</td></tr>");
                    }
                    sb.AppendLine("</tbody></table>");
                }

                // Footer (copiado del layout)
                sb.AppendLine("</section>\n</div>\n<footer class=\"site-footer\">\n  <div class=\"footer-inner\">\n    <div class=\"footer-brand\">\n      <span>Clínica Dental y Especialidades<br><strong>Dra. Rebeca</strong></span>\n    </div>\n    <p class=\"footer-tagline\">Sonríe con confianza</p>\n    <p class=\"footer-copy\">&copy; " + DateTime.Now.Year + " — Todos los derechos reservados</p>\n  </div>\n</footer>\n</div>");

                sb.AppendLine("<script src=\"/Scripts/jquery-3.6.0.min.js\"></script>");
                sb.AppendLine("<script src=\"/Scripts/bootstrap.min.js\"></script>");
                sb.AppendLine("</body></html>");

                return Content(sb.ToString(), "text/html");
            }
        }

        // GET: Reporteria/ReportesInventario
        public ActionResult ReportesInventario()
        {
            return View("Inventario");
        }

        // GET: Reporteria/ReportesProductosUtilizados
        public ActionResult ReportesProductosUtilizados()
        {
            using (var contexto = new Contexto())
            {
                var lista = contexto.UsoProductos.OrderBy(u => u.ID_USO).ToList();

                var sb = new System.Text.StringBuilder();
                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang=\"es\">\n<head>\n    <meta charset=\"utf-8\" />\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>Reporte de Productos Utilizados - Clínica Dental</title>\n    <link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" />\n</head>\n<body>");

                sb.AppendLine("<div class=\"page-wrapper\">\n<nav class=\"main-nav\">\n  <div class=\"nav-inner\">\n    <div class=\"navbar-title\">Clínica dental y especialidades</div>\n    <div class=\"nav-links-wrapper\" id=\"mainNavLinks\">\n      <ul class=\"nav-links\">\n        <li><a class=\"nav-link\" href=\"/\">Inicio</a></li>\n        <li><a class=\"nav-link\" href=\"/Reporteria\">Reporteria</a></li>\n      </ul>\n    </div>\n  </div>\n</nav>");

                sb.AppendLine("<div class=\"usuarios-page\">\n  <div class=\"usuarios-header\">\n    <div>\n      <h1 class=\"usuarios-titulo\">Reporte de Productos Utilizados</h1>\n      <p class=\"usuarios-subtitulo\">Listado completo de la tabla FIDE_USO_PRODUCTO_TB</p>\n    </div>\n    <div>\n      <a href=\"/Reporteria\" class=\"btn-usuario-primary\">← Volver a Reportería</a>\n    </div>\n  </div>\n  <section class=\"usuarios-table-card rep-contenido\">\n");

                if (lista == null || !lista.Any())
                {
                    sb.AppendLine("<p class=\"rep-placeholder\">No hay registros en FIDE_USO_PRODUCTO_TB.</p>");
                }
                else
                {
                    sb.AppendLine("<table class=\"table\" style=\"width:100%;border-collapse:collapse;\"><thead><tr><th>ID_USO</th><th>ID_PRODUCTO</th><th>ID_PROCEDIMIENTO</th><th>CANTIDAD</th><th>ID_ESTADO</th></tr></thead><tbody>");
                    foreach (var r in lista)
                    {
                        sb.AppendLine($"<tr><td>{r.ID_USO}</td><td>{r.ID_PRODUCTO}</td><td>{r.ID_PROCEDIMIENTO}</td><td>{(r.CANTIDAD.HasValue? r.CANTIDAD.Value.ToString() : "")}</td><td>{r.ID_ESTADO}</td></tr>");
                    }
                    sb.AppendLine("</tbody></table>");
                }

                sb.AppendLine("</section>\n</div>\n<footer class=\"site-footer\">\n  <div class=\"footer-inner\">\n    <div class=\"footer-brand\">\n      <span>Clínica Dental y Especialidades</span>\n    </div>\n    <p class=\"footer-copy\">&copy; " + DateTime.Now.Year + "</p>\n  </div>\n</footer>\n</div>");

                sb.AppendLine("</body></html>");

                return Content(sb.ToString(), "text/html");
            }
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
