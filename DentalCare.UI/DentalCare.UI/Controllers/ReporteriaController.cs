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
using DentaCare.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
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
        private IObtenerTodosLosUsuariosLN _obtenerTodosLosUsuariosLN;

        // Estilo compartido teal para los reportes generados como HTML crudo.
        private const string EstiloReporte =
            ".reporte-page{min-height:100vh;padding:40px 32px 60px;background:linear-gradient(180deg,#F4FBFA 0%,#ffffff 220px);font-family:'DM Sans',sans-serif;}" +
            ".reporte-header{display:flex;justify-content:space-between;align-items:center;flex-wrap:wrap;gap:20px;padding:30px;border-radius:20px;background:linear-gradient(135deg,#16302D 0%,#1F6B75 60%,#2C8C99 100%);color:white;margin-bottom:25px;box-shadow:0 10px 30px rgba(22,48,45,.25);}" +
            ".reporte-header h1{font-size:1.8rem;font-weight:700;margin:0;color:white;}" +
            ".reporte-header p{margin-top:8px;color:#CFE8E6;font-size:.92rem;}" +
            ".btn-volver-reporte{background:rgba(255,255,255,.15);border:1px solid rgba(255,255,255,.35);color:white;text-decoration:none;padding:10px 18px;border-radius:8px;font-weight:600;white-space:nowrap;}" +
            ".btn-volver-reporte:hover{background:rgba(255,255,255,.25);color:white;text-decoration:none;}" +
            ".reporte-card{background:white;border-radius:16px;padding:30px;box-shadow:0 8px 30px rgba(0,0,0,.06);border-top:4px solid #16302D;overflow-x:auto;max-width:1100px;margin:0 auto;}" +
            ".reporte-card label{font-weight:600;color:#374151;margin-right:6px;}" +
            ".reporte-card input[type=date]{height:40px;border-radius:10px;border:1px solid #CFE8E6;padding:0 10px;}" +
            ".btn-filtrar-reporte{background:linear-gradient(135deg,#3E8E7E,#2F6E62);border:none;color:white;padding:10px 20px;border-radius:10px;font-weight:600;cursor:pointer;}" +
            ".tabla-reporte{width:100%;border-collapse:collapse;margin-top:20px;}" +
            ".tabla-reporte th{background:#EAF5F3;color:#16302D;text-align:left;padding:12px;font-size:.8rem;border-bottom:2px solid #CFE8E6;}" +
            ".tabla-reporte td{padding:12px;border-bottom:1px solid #edf2f7;color:#374151;}" +
            ".rep-placeholder{text-align:center;color:#6b7280;padding:40px;}" +
            ".rep-paginacion{text-align:center;margin-top:18px;}" +
            ".rep-paginacion a{display:inline-block;background:#2F6E62;color:white;padding:8px 15px;border-radius:8px;text-decoration:none;margin:0 6px;}" +
            ".rep-paginacion a:hover{background:#1F4E44;color:white;text-decoration:none;}" +
            ".rep-paginacion span{font-weight:600;color:#16302D;margin:0 8px;}";

        public ReporteriaController()
        {
            _reporteProductosLN = new ReporteProductosLN(new ReporteProductosAD(new Contexto()));
            _reporteLotesLN = new ReporteLotesLN(new ReporteLotesAD(new Contexto()));
            _reporteCitasLN = new ReporteCitasLN(new ReporteCitasAD(new Contexto()));
            _obtenerTodosLosUsuariosLN = new ObtenerTodosLosUsuariosLN();
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

                var sb = new System.Text.StringBuilder();

                sb.Append("<!DOCTYPE html><html lang=\"es\"><head>");
                sb.Append("<meta charset=\"utf-8\" />");
                sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sb.Append("<title>Reportes de Tratamientos - Clinica Dental Dra. Rebeca</title>");
                sb.Append("<link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" />");
                sb.Append("<link href=\"https://fonts.googleapis.com/css2?family=Playfair+Display:wght@400;600;700&family=DM+Sans:wght@300;400;500;600&display=swap\" rel=\"stylesheet\" />");
                sb.Append("<style>" + EstiloReporte + "</style>");
                sb.Append("</head><body>");

                sb.Append("<nav class=\"main-nav\"><div class=\"nav-inner\">");
                sb.Append("<div class=\"navbar-title\">Clinica dental y especialidades Dra. Rebeca</div>");
                sb.Append("<div class=\"nav-links-wrapper\" id=\"mainNavLinks\"><ul class=\"nav-links\">");
                sb.Append("<li><a class=\"nav-link\" href=\"/\">Inicio</a></li>");
                sb.Append("<li><a class=\"nav-link\" href=\"/Reporteria\">Reporteria</a></li>");
                sb.Append("</ul></div></div></nav>");

                sb.Append("<div class=\"page-wrapper\"><div class=\"reporte-page\">");
                sb.Append("<div class=\"reporte-header\"><div>");
                sb.Append("<h1><i class=\"fa fa-tooth\"></i> Reportes de Tratamientos</h1>");
                sb.Append("<p>Ver el historial de tratamientos y filtrarlo por fecha.</p>");
                sb.Append("</div><div><a href=\"/Reporteria\" class=\"btn-volver-reporte\">&larr; Volver a Reporteria</a></div>");
                sb.Append("</div>");

                sb.Append("<div class=\"reporte-card\">");
                sb.Append("<form method=\"get\" action=\"/Reporteria/ReportesTratamientos\">");
                sb.Append("<label>Desde:</label> <input type=\"date\" name=\"desde\" value=\"" + (desde.HasValue ? desde.Value.ToString("yyyy-MM-dd") : "") + "\" />");
                sb.Append("<label style=\"margin-left:12px;\">Hasta:</label> <input type=\"date\" name=\"hasta\" value=\"" + (hasta.HasValue ? hasta.Value.ToString("yyyy-MM-dd") : "") + "\" />");
                sb.Append("<button type=\"submit\" class=\"btn-filtrar-reporte\" style=\"margin-left:12px;\">Filtrar</button>");
                sb.Append("</form>");

                if (lista == null || !lista.Any())
                {
                    sb.Append("<p class=\"rep-placeholder\">No hay tratamientos para mostrar.</p>");
                }
                else
                {
                    sb.Append("<table class=\"tabla-reporte\"><thead><tr>");
                    sb.Append("<th>ID</th><th>Descripcion</th><th>Fecha Inicio</th><th>Fecha Fin</th><th>Estado</th><th>Monto</th><th>ID Cita</th>");
                    sb.Append("</tr></thead><tbody>");

                    int contador = 1;

                    foreach (var t in lista)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + contador + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(t.Descripcion) + "</td>");
                        sb.Append("<td>" + (t.FechaInicio.HasValue ? t.FechaInicio.Value.ToString("dd/MM/yyyy") : "-") + "</td>");
                        sb.Append("<td>" + (t.FechaFin.HasValue ? t.FechaFin.Value.ToString("dd/MM/yyyy") : "-") + "</td>");
                        sb.Append("<td>" + t.IdEstado + "</td>");
                        sb.Append("<td>" + (t.Monto.HasValue ? t.Monto.Value.ToString("F2") : "-") + "</td>");
                        sb.Append("<td>" + (t.IdCita.HasValue ? t.IdCita.ToString() : "-") + "</td>");
                        sb.Append("</tr>");

                        contador++;
                    }

                    sb.Append("</tbody></table>");
                }

                sb.Append("</div>"); // reporte-card
                sb.Append("</div>"); // reporte-page

                sb.Append("<footer class=\"site-footer\"><div class=\"footer-inner\">");
                sb.Append("<div class=\"footer-brand\"><span>Clinica Dental y Especialidades<br><strong>Dra. Rebeca</strong></span></div>");
                sb.Append("<p class=\"footer-tagline\">Sonrie con confianza</p>");
                sb.Append("<p class=\"footer-copy\">&copy; " + DateTime.Now.Year + " - Todos los derechos reservados</p>");
                sb.Append("</div></footer>");
                sb.Append("</div>"); // page-wrapper

                sb.Append("<script src=\"/Scripts/jquery-3.6.0.min.js\"></script>");
                sb.Append("<script src=\"/Scripts/bootstrap.min.js\"></script>");
                sb.Append("</body></html>");

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

                sb.Append("<!DOCTYPE html><html lang=\"es\"><head>");
                sb.Append("<meta charset=\"utf-8\" />");
                sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sb.Append("<title>Reporte de Productos Utilizados - Clinica Dental</title>");
                sb.Append("<link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" />");
                sb.Append("<style>" + EstiloReporte + "</style>");
                sb.Append("</head><body>");

                sb.Append("<nav class=\"main-nav\"><div class=\"nav-inner\">");
                sb.Append("<div class=\"navbar-title\">Clinica dental y especialidades Dra. Rebeca</div>");
                sb.Append("<div class=\"nav-links-wrapper\" id=\"mainNavLinks\"><ul class=\"nav-links\">");
                sb.Append("<li><a class=\"nav-link\" href=\"/\">Inicio</a></li>");
                sb.Append("<li><a class=\"nav-link\" href=\"/Reporteria\">Reporteria</a></li>");
                sb.Append("</ul></div></div></nav>");

                sb.Append("<div class=\"page-wrapper\"><div class=\"reporte-page\">");
                sb.Append("<div class=\"reporte-header\"><div>");
                sb.Append("<h1><i class=\"fa fa-flask\"></i> Reporte de Productos Utilizados</h1>");
                sb.Append("<p>Listado completo de productos usados en tratamientos.</p>");
                sb.Append("</div><div><a href=\"/Reporteria\" class=\"btn-volver-reporte\">&larr; Volver a Reporteria</a></div>");
                sb.Append("</div>");

                sb.Append("<div class=\"reporte-card\">");

                if (lista == null || !lista.Any())
                {
                    sb.Append("<p class=\"rep-placeholder\">No hay registros para mostrar.</p>");
                }
                else
                {
                    sb.Append("<table class=\"tabla-reporte\"><thead><tr>");
                    sb.Append("<th>ID</th><th>ID Producto</th><th>ID Procedimiento</th><th>Cantidad</th><th>Estado</th>");
                    sb.Append("</tr></thead><tbody>");

                    int contador = 1;

                    foreach (var r in lista)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + contador + "</td>");
                        sb.Append("<td>" + r.ID_PRODUCTO + "</td>");
                        sb.Append("<td>" + r.ID_PROCEDIMIENTO + "</td>");
                        sb.Append("<td>" + (r.CANTIDAD.HasValue ? r.CANTIDAD.Value.ToString() : "-") + "</td>");
                        sb.Append("<td>" + r.ID_ESTADO + "</td>");
                        sb.Append("</tr>");

                        contador++;
                    }

                    sb.Append("</tbody></table>");
                }

                sb.Append("</div>"); // reporte-card
                sb.Append("</div>"); // reporte-page

                sb.Append("<footer class=\"site-footer\"><div class=\"footer-inner\">");
                sb.Append("<div class=\"footer-brand\"><span>Clinica Dental y Especialidades</span></div>");
                sb.Append("<p class=\"footer-copy\">&copy; " + DateTime.Now.Year + "</p>");
                sb.Append("</div></footer>");
                sb.Append("</div>"); // page-wrapper

                sb.Append("</body></html>");

                return Content(sb.ToString(), "text/html");
            }
        }

        // GET: Reporteria/ReportesPagos?desde=yyyy-MM-dd&hasta=yyyy-MM-dd
        public ActionResult ReportesPagos(DateTime? desde, DateTime? hasta)
        {
            using (var contexto = new Contexto())
            {
                var query = contexto.Gastos.AsQueryable();

                if (desde.HasValue)
                    query = query.Where(g => g.Fecha >= desde.Value);

                if (hasta.HasValue)
                    query = query.Where(g => g.Fecha <= hasta.Value);

                var lista = (from g in query
                             join estado in contexto.Estados on g.IdEstado equals estado.IdEstado
                             select new
                             {
                                 g.IdGasto,
                                 g.Descripcion,
                                 g.Monto,
                                 g.Fecha,
                                 NombreEstado = estado.NombreEstado
                             })
                            .OrderByDescending(x => x.Fecha)
                            .ToList();

                var sb = new System.Text.StringBuilder();

                sb.Append("<!DOCTYPE html><html lang=\"es\"><head>");
                sb.Append("<meta charset=\"utf-8\" />");
                sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sb.Append("<title>Reportes de Pagos - Clinica Dental</title>");
                sb.Append("<link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" />");
                sb.Append("<style>" + EstiloReporte + "</style>");
                sb.Append("</head><body>");

                sb.Append("<nav class=\"main-nav\"><div class=\"nav-inner\">");
                sb.Append("<div class=\"navbar-title\">Clinica dental y especialidades Dra. Rebeca</div>");
                sb.Append("<div class=\"nav-links-wrapper\" id=\"mainNavLinks\"><ul class=\"nav-links\">");
                sb.Append("<li><a class=\"nav-link\" href=\"/\">Inicio</a></li>");
                sb.Append("<li><a class=\"nav-link\" href=\"/Reporteria\">Reporteria</a></li>");
                sb.Append("</ul></div></div></nav>");

                sb.Append("<div class=\"page-wrapper\"><div class=\"reporte-page\">");
                sb.Append("<div class=\"reporte-header\"><div>");
                sb.Append("<h1><i class=\"fa fa-money\"></i> Reportes de Pagos</h1>");
                sb.Append("<p>Listado de gastos registrados. Filtra por fechas si lo necesitas.</p>");
                sb.Append("</div><div><a href=\"/Reporteria\" class=\"btn-volver-reporte\">&larr; Volver a Reporteria</a></div>");
                sb.Append("</div>");

                sb.Append("<div class=\"reporte-card\">");
                sb.Append("<form method=\"get\" action=\"/Reporteria/ReportesPagos\">");
                sb.Append("<label>Desde:</label> <input type=\"date\" name=\"desde\" value=\"" + (desde.HasValue ? desde.Value.ToString("yyyy-MM-dd") : "") + "\" />");
                sb.Append("<label style=\"margin-left:12px;\">Hasta:</label> <input type=\"date\" name=\"hasta\" value=\"" + (hasta.HasValue ? hasta.Value.ToString("yyyy-MM-dd") : "") + "\" />");
                sb.Append("<button type=\"submit\" class=\"btn-filtrar-reporte\" style=\"margin-left:12px;\">Filtrar</button>");
                sb.Append("</form>");

                if (lista == null || !lista.Any())
                {
                    sb.Append("<p class=\"rep-placeholder\">No hay registros para las fechas seleccionadas.</p>");
                }
                else
                {
                    sb.Append("<table class=\"tabla-reporte\"><thead><tr>");
                    sb.Append("<th>ID</th><th>Descripcion</th><th>Monto</th><th>Fecha</th><th>Estado</th>");
                    sb.Append("</tr></thead><tbody>");

                    int contador = 1;

                    foreach (var g in lista)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + contador + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(g.Descripcion) + "</td>");
                        sb.Append("<td>" + (g.Monto.HasValue ? g.Monto.Value.ToString("F2") : "-") + "</td>");
                        sb.Append("<td>" + (g.Fecha.HasValue ? g.Fecha.Value.ToString("dd/MM/yyyy") : "-") + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(g.NombreEstado) + "</td>");
                        sb.Append("</tr>");

                        contador++;
                    }

                    sb.Append("</tbody></table>");
                }

                sb.Append("</div>"); // reporte-card
                sb.Append("</div>"); // reporte-page

                sb.Append("<footer class=\"site-footer\"><div class=\"footer-inner\">");
                sb.Append("<div class=\"footer-brand\"><span>Clinica Dental y Especialidades<br><strong>Dra. Rebeca</strong></span></div>");
                sb.Append("<p class=\"footer-tagline\">Sonrie con confianza</p>");
                sb.Append("<p class=\"footer-copy\">&copy; " + DateTime.Now.Year + " - Todos los derechos reservados</p>");
                sb.Append("</div></footer>");
                sb.Append("</div>"); // page-wrapper

                sb.Append("<script src=\"/Scripts/jquery-3.6.0.min.js\"></script>");
                sb.Append("<script src=\"/Scripts/bootstrap.min.js\"></script>");
                sb.Append("</body></html>");

                return Content(sb.ToString(), "text/html");
            }
        }

        // GET: Reporteria/ReportesPacientesAtendidos
        // Muestra las citas con fecha pasada (antes de hoy) con paginacion
        public ActionResult ReportesPacientesAtendidos(int page = 1, int pageSize = 20)
        {
            using (var contexto = new Contexto())
            {
                var query = contexto.Citas.AsQueryable();

                var hoy = DateTime.Today;
                query = query.Where(c => c.Fecha.HasValue && c.Fecha.Value < hoy);

                var total = query.Count();
                var totalPages = (int)Math.Ceiling(total / (double)pageSize);

                // Construir lista con nombres legibles (paciente, doctor, motivo, estado)
                var raw = (from cita in contexto.Citas
                           join uc in contexto.UsuarioCitas on cita.IdCita equals uc.IdCita into ucGrupo
                           from uc in ucGrupo.DefaultIfEmpty()
                           join paciente in contexto.Usuarios on uc.IdUsuario equals paciente.IdUsuario into pacienteGrupo
                           from paciente in pacienteGrupo.DefaultIfEmpty()
                           join doctor in contexto.Usuarios on cita.IdDoctor equals doctor.IdUsuario into doctorGrupo
                           from doctor in doctorGrupo.DefaultIfEmpty()
                           join motivo in contexto.MotivosCita on cita.IdMotivo equals motivo.IdMotivo into motivoGrp
                           from motivo in motivoGrp.DefaultIfEmpty()
                           join estado in contexto.Estados on cita.IdEstado equals estado.IdEstado into estadoGrp
                           from estado in estadoGrp.DefaultIfEmpty()
                           where cita.Fecha.HasValue && cita.Fecha.Value < hoy
                           select new
                           {
                               cita.IdCita,
                               cita.Fecha,
                               cita.Hora,
                               NombrePaciente = paciente != null ? paciente.Nombre + " " + paciente.PrimerApellido : "Sin asignar",
                               NombreDoctor = doctor != null ? doctor.Nombre + " " + doctor.PrimerApellido : "Sin asignar",
                               NombreMotivo = motivo != null ? motivo.Descripcion : "-",
                               NombreEstado = estado != null ? estado.NombreEstado : "-"
                           }).ToList();

                // Paginación sobre la lista ya proyectada
                var lista = raw.OrderByDescending(r => r.Fecha).ThenByDescending(r => r.Hora)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .Select(r => new CitaReporteDto
                               {
                                   IdCita = r.IdCita,
                                   Fecha = r.Fecha,
                                   HoraString = r.Hora.HasValue ? r.Hora.Value.ToString(@"hh\:mm") : "—",
                                   NombrePaciente = r.NombrePaciente,
                                   NombreDoctor = r.NombreDoctor,
                                   NombreMotivo = r.NombreMotivo,
                                   NombreEstado = r.NombreEstado
                               })
                               .ToList();

                var sb = new System.Text.StringBuilder();

                sb.Append("<!DOCTYPE html><html lang=\"es\"><head>");
                sb.Append("<meta charset=\"utf-8\" />");
                sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sb.Append("<title>Pacientes Atendidos - Clinica Dental</title>");
                sb.Append("<link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" />");
                sb.Append("<style>" + EstiloReporte + "</style>");
                sb.Append("</head><body>");

                sb.Append("<nav class=\"main-nav\"><div class=\"nav-inner\">");
                sb.Append("<div class=\"navbar-title\">Clinica dental y especialidades Dra. Rebeca</div>");
                sb.Append("<div class=\"nav-links-wrapper\" id=\"mainNavLinks\"><ul class=\"nav-links\">");
                sb.Append("<li><a class=\"nav-link\" href=\"/\">Inicio</a></li>");
                sb.Append("<li><a class=\"nav-link\" href=\"/Reporteria\">Reporteria</a></li>");
                sb.Append("</ul></div></div></nav>");

                sb.Append("<div class=\"page-wrapper\"><div class=\"reporte-page\">");
                sb.Append("<div class=\"reporte-header\"><div>");
                sb.Append("<h1><i class=\"fa fa-user-md\"></i> Pacientes Atendidos</h1>");
                sb.Append("<p>Listado de citas ya realizadas (fechas pasadas).</p>");
                sb.Append("</div><div><a href=\"/Reporteria\" class=\"btn-volver-reporte\">&larr; Volver a Reporteria</a></div>");
                sb.Append("</div>");

                sb.Append("<div class=\"reporte-card\">");

                if (lista == null || !lista.Any())
                {
                    sb.Append("<p class=\"rep-placeholder\">No hay citas pasadas para mostrar.</p>");
                }
                else
                {
                    sb.Append("<table class=\"tabla-reporte\"><thead><tr>");
                    sb.Append("<th>ID</th><th>Paciente</th><th>Doctor</th><th>Fecha</th><th>Hora</th><th>Motivo</th><th>Estado</th>");
                    sb.Append("</tr></thead><tbody>");

                    // Contador continuo entre paginas: la pagina 2 sigue en 21, 22, 23...
                    int contador = ((page - 1) * pageSize) + 1;

                    foreach (var r in lista)
                    {
                        var fecha = r.Fecha.HasValue ? r.Fecha.Value.ToString("dd/MM/yyyy") : "-";
                        var hora = string.IsNullOrEmpty(r.HoraString) ? "-" : r.HoraString;

                        sb.Append("<tr>");
                        sb.Append("<td>" + contador + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombrePaciente) + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombreDoctor) + "</td>");
                        sb.Append("<td>" + fecha + "</td>");
                        sb.Append("<td>" + hora + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombreMotivo) + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombreEstado) + "</td>");
                        sb.Append("</tr>");

                        contador++;
                    }

                    sb.Append("</tbody></table>");

                    sb.Append("<div class=\"rep-paginacion\">");

                    if (page > 1)
                    {
                        sb.Append("<a href=\"/Reporteria/ReportesPacientesAtendidos?page=" + (page - 1) + "&pageSize=" + pageSize + "\">&larr; Anterior</a>");
                    }

                    sb.Append("<span>Pagina " + page + " de " + Math.Max(totalPages, 1) + "</span>");

                    if (page < totalPages)
                    {
                        sb.Append("<a href=\"/Reporteria/ReportesPacientesAtendidos?page=" + (page + 1) + "&pageSize=" + pageSize + "\">Siguiente &rarr;</a>");
                    }

                    sb.Append("</div>");
                }

                sb.Append("</div>"); // reporte-card
                sb.Append("</div>"); // reporte-page

                sb.Append("<footer class=\"site-footer\"><div class=\"footer-inner\">");
                sb.Append("<div class=\"footer-brand\"><span>Clinica Dental y Especialidades<br><strong>Dra. Rebeca</strong></span></div>");
                sb.Append("<p class=\"footer-tagline\">Sonrie con confianza</p>");
                sb.Append("<p class=\"footer-copy\">&copy; " + DateTime.Now.Year + " - Todos los derechos reservados</p>");
                sb.Append("</div></footer>");
                sb.Append("</div>"); // page-wrapper

                sb.Append("<script src=\"/Scripts/jquery-3.6.0.min.js\"></script>");
                sb.Append("<script src=\"/Scripts/bootstrap.min.js\"></script>");
                sb.Append("</body></html>");

                return Content(sb.ToString(), "text/html");
            }
        }

        // GET: Reporteria/ReportesUsuarios
        public ActionResult ReportesUsuarios()
        {
            var lista = _obtenerTodosLosUsuariosLN.Obtener();
            return View("~/Views/Usuario/ObtenerTodosLosUsuarios.cshtml", lista);
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