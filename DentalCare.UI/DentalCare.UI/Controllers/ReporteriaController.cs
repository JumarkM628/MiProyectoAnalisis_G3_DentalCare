using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DentaCare.LogicaDeNegocio.Reporteria.Citas;
using DentaCare.LogicaDeNegocio.Reporteria.Expediente;
using DentaCare.LogicaDeNegocio.Reporteria.Finanzas;
using DentaCare.LogicaDeNegocio.Reporteria.Producto;
using DentaCare.LogicaDeNegocio.Reporteria.Usuario;
using DentaCare.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Citas;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Expediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Finanzas;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Usuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
using DentalCare.Abstraccion.Modelo.Reporteria;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Citas.Reporteria;
using DentalCare.AccesoADatos.Reporteria.Cita;
using DentalCare.AccesoADatos.Reporteria.Expediente;
using DentalCare.AccesoADatos.Reporteria.Finanzas;
using DentalCare.AccesoADatos.Reporteria.Inventario;
using DentalCare.AccesoADatos.Reporteria.Producto;
using DentalCare.AccesoADatos.Reporteria.Usuario;
using Microsoft.AspNet.Identity;

namespace DentalCare.UI.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class ReporteriaController : Controller
    {
        private readonly IReporteProductosLN _reporteProductosLN;
        private readonly IReporteLotesLN _reporteLotesLN;
        private readonly IReporteCitasLN _reporteCitasLN;
        private readonly IReporteBajoStockLN _reporteBajoStockLN;
        private readonly IReporteProductosVencerLN _reporteProductosVencerLN;
        private readonly IReporteCitasCanceladasLN _reporteCitasCanceladasLN;
        private readonly IReporteProcedimientosLN _reporteProcedimientosLN;
        private readonly IReporteGastosLN _reporteGastosLN;
        private readonly IHistorialDoctoraLN _historialDoctoraLN;
        private IObtenerTodosLosUsuariosLN _obtenerTodosLosUsuariosLN;

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
            _reporteBajoStockLN = new ReporteBajoStockLN(new ReporteBajoStockAD(new Contexto()));
            _reporteProductosVencerLN = new ReporteProductosVencerLN(new ReporteProductosVencerAD(new Contexto()));
            _reporteCitasCanceladasLN = new ReporteCitasCanceladasLN(new ReporteCitasCanceladasAD(new Contexto()));
            _reporteProcedimientosLN = new ReporteProcedimientosLN(new ReporteProcedimientosAD(new Contexto()));
            _reporteGastosLN = new ReporteGastosLN(new ReporteGastosAD(new Contexto()));
            _historialDoctoraLN = new HistorialDoctoraLN(new HistorialDoctoraAD(new Contexto()));
            _obtenerTodosLosUsuariosLN = new ObtenerTodosLosUsuariosLN();
        }

        public ActionResult Index() => View();
        public ActionResult ReportesProductos() => View();

        public ActionResult ProductosMasUtilizados() => PartialView(_reporteProductosLN.ObtenerMasUtilizados());
        public ActionResult ProductosMenosUtilizados() => PartialView(_reporteProductosLN.ObtenerMenosUtilizados());
        public ActionResult ProductosMasComprados() => PartialView(_reporteProductosLN.ObtenerMasComprados());
        public ActionResult ProductosMenosComprados() => PartialView(_reporteProductosLN.ObtenerMenosComprados());
        public ActionResult HistorialPorTratamiento() => PartialView(_reporteProductosLN.ObtenerHistorialPorTratamiento());
        public ActionResult LotesMasUtilizados() => PartialView(_reporteLotesLN.ObtenerLotesMasUtilizados());
        public ActionResult LotesMenosUtilizados() => PartialView(_reporteLotesLN.ObtenerLotesMenosUtilizados());
        public ActionResult LotesMasComprados() => PartialView(_reporteLotesLN.ObtenerLotesMasComprados());
        public ActionResult LotesMenosComprados() => PartialView(_reporteLotesLN.ObtenerLotesMenosComprados());
        public ActionResult HistorialLotePorTratamiento() => PartialView(_reporteLotesLN.ObtenerHistorialLotePorTratamiento());
        public ActionResult ReportesCitas() => View();

        public ActionResult ReportesTratamientos(DateTime? desde, DateTime? hasta)
        {
            using (var contexto = new Contexto())
            {
                var query = contexto.PlanesTratamiento.AsQueryable();
                if (desde.HasValue) query = query.Where(t => t.FechaInicio >= desde.Value);
                if (hasta.HasValue) query = query.Where(t => t.FechaInicio <= hasta.Value);

                var raw = (from t in query
                           join estado in contexto.Estados on t.IdEstado equals estado.IdEstado into estadoGrp
                           from estado in estadoGrp.DefaultIfEmpty()
                           select new { Tratamiento = t, NombreEstado = estado != null ? estado.NombreEstado : "-" })
                          .OrderBy(x => x.Tratamiento.FechaInicio).ToList();

                var sb = new System.Text.StringBuilder();
                sb.Append("<!DOCTYPE html><html lang=\"es\"><head><meta charset=\"utf-8\" /><title>Reportes de Tratamientos</title>");
                sb.Append("<link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" /><style>" + EstiloReporte + "</style></head><body>");
                sb.Append("<div class=\"page-wrapper\"><div class=\"reporte-page\">");
                sb.Append("<div class=\"reporte-header\"><div><h1>Reportes de Tratamientos</h1></div>");
                sb.Append("<div><a href=\"/Reporteria\" class=\"btn-volver-reporte\">&larr; Volver</a></div></div>");
                sb.Append("<div class=\"reporte-card\">");
                sb.Append("<form method=\"get\" action=\"/Reporteria/ReportesTratamientos\">");
                sb.Append("<label>Desde:</label> <input type=\"date\" name=\"desde\" value=\"" + (desde.HasValue ? desde.Value.ToString("yyyy-MM-dd") : "") + "\" />");
                sb.Append("<label style=\"margin-left:12px;\">Hasta:</label> <input type=\"date\" name=\"hasta\" value=\"" + (hasta.HasValue ? hasta.Value.ToString("yyyy-MM-dd") : "") + "\" />");
                sb.Append("<button type=\"submit\" class=\"btn-filtrar-reporte\" style=\"margin-left:12px;\">Filtrar</button></form>");

                if (!raw.Any())
                {
                    sb.Append("<p class=\"rep-placeholder\">No hay tratamientos para mostrar.</p>");
                }
                else
                {
                    sb.Append("<table class=\"tabla-reporte\"><thead><tr><th>ID</th><th>Descripcion</th><th>Fecha Inicio</th><th>Fecha Fin</th><th>Estado</th><th>Monto</th><th>ID Cita</th></tr></thead><tbody>");
                    int i = 1;
                    foreach (var item in raw)
                    {
                        var t = item.Tratamiento;
                        sb.Append("<tr>");
                        sb.Append("<td>" + i++ + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(t.Descripcion) + "</td>");
                        sb.Append("<td>" + (t.FechaInicio.HasValue ? t.FechaInicio.Value.ToString("dd/MM/yyyy") : "-") + "</td>");
                        sb.Append("<td>" + (t.FechaFin.HasValue ? t.FechaFin.Value.ToString("dd/MM/yyyy") : "-") + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(item.NombreEstado) + "</td>");
                        sb.Append("<td>" + (t.Monto.HasValue ? t.Monto.Value.ToString("F2") : "-") + "</td>");
                        sb.Append("<td>" + (t.IdCita.HasValue ? t.IdCita.ToString() : "-") + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody></table>");
                }
                sb.Append("</div></div></div></body></html>");
                return Content(sb.ToString(), "text/html");
            }
        }

        public ActionResult ReportesInventario()
        {
            using (var ctx = new Contexto())
            {
                var lista = (from p in ctx.Productos
                             join est in ctx.Estados on p.ID_ESTADO equals est.IdEstado into estGrp
                             from est in estGrp.DefaultIfEmpty()
                             select new InventarioReporteDto
                             {
                                 Producto = p.NOMBRE_PRODUCTO,
                                 Stock = p.STOCK_ACTUAL ?? 0,
                                 Estado = est != null ? est.NombreEstado : "-"
                             }).OrderBy(x => x.Producto).ToList();
                return View("Inventario", lista);
            }
        }

        public ActionResult ReportesProductosUtilizados()
        {
            using (var contexto = new Contexto())
            {
                var raw = (from uso in contexto.UsoProductos
                           join prod in contexto.Productos on uso.ID_PRODUCTO equals prod.ID_PRODUCTO into prodGrp
                           from prod in prodGrp.DefaultIfEmpty()
                           join proc in contexto.Procedimientos on uso.ID_PROCEDIMIENTO equals proc.ID_PROCEDIMIENTO into procGrp
                           from proc in procGrp.DefaultIfEmpty()
                           join estado in contexto.Estados on uso.ID_ESTADO equals estado.IdEstado into estadoGrp
                           from estado in estadoGrp.DefaultIfEmpty()
                           select new
                           {
                               Uso = uso,
                               NombreProducto = prod != null ? prod.NOMBRE_PRODUCTO : "-",
                               ProcedimientoDescripcion = proc != null ? proc.DESCRIPCION : "-",
                               NombreEstado = estado != null ? estado.NombreEstado : "-"
                           }).OrderBy(x => x.Uso.ID_USO).ToList();

                var sb = new System.Text.StringBuilder();
                sb.Append("<!DOCTYPE html><html lang=\"es\"><head><meta charset=\"utf-8\" /><title>Productos Utilizados</title>");
                sb.Append("<link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" /><style>" + EstiloReporte + "</style></head><body>");
                sb.Append("<div class=\"page-wrapper\"><div class=\"reporte-page\">");
                sb.Append("<div class=\"reporte-header\"><div><h1>Reporte de Productos Utilizados</h1></div>");
                sb.Append("<div><a href=\"/Reporteria\" class=\"btn-volver-reporte\">&larr; Volver</a></div></div>");
                sb.Append("<div class=\"reporte-card\">");
                if (!raw.Any())
                {
                    sb.Append("<p class=\"rep-placeholder\">No hay registros para mostrar.</p>");
                }
                else
                {
                    sb.Append("<table class=\"tabla-reporte\"><thead><tr><th>ID</th><th>Producto</th><th>Procedimiento</th><th>Cantidad</th><th>Estado</th></tr></thead><tbody>");
                    int i = 1;
                    foreach (var item in raw)
                    {
                        sb.Append("<tr><td>" + i++ + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(item.NombreProducto) + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(item.ProcedimientoDescripcion) + "</td>");
                        sb.Append("<td>" + (item.Uso.CANTIDAD.HasValue ? item.Uso.CANTIDAD.Value.ToString() : "-") + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(item.NombreEstado) + "</td></tr>");
                    }
                    sb.Append("</tbody></table>");
                }
                sb.Append("</div></div></div></body></html>");
                return Content(sb.ToString(), "text/html");
            }
        }

        public ActionResult ReportesPagos(DateTime? desde, DateTime? hasta)
        {
            using (var contexto = new Contexto())
            {
                var query = contexto.Gastos.AsQueryable();
                if (desde.HasValue) query = query.Where(g => g.Fecha >= desde.Value);
                if (hasta.HasValue) query = query.Where(g => g.Fecha <= hasta.Value);

                var lista = (from g in query
                             join estado in contexto.Estados on g.IdEstado equals estado.IdEstado
                             select new { g.IdGasto, g.Descripcion, g.Monto, g.Fecha, NombreEstado = estado.NombreEstado })
                            .OrderByDescending(x => x.Fecha).ToList();

                var sb = new System.Text.StringBuilder();
                sb.Append("<!DOCTYPE html><html lang=\"es\"><head><meta charset=\"utf-8\" /><title>Reportes de Pagos</title>");
                sb.Append("<link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" /><style>" + EstiloReporte + "</style></head><body>");
                sb.Append("<div class=\"page-wrapper\"><div class=\"reporte-page\">");
                sb.Append("<div class=\"reporte-header\"><div><h1>Reportes de Pagos</h1></div>");
                sb.Append("<div><a href=\"/Reporteria\" class=\"btn-volver-reporte\">&larr; Volver</a></div></div>");
                sb.Append("<div class=\"reporte-card\">");
                sb.Append("<form method=\"get\" action=\"/Reporteria/ReportesPagos\">");
                sb.Append("<label>Desde:</label> <input type=\"date\" name=\"desde\" value=\"" + (desde.HasValue ? desde.Value.ToString("yyyy-MM-dd") : "") + "\" />");
                sb.Append("<label style=\"margin-left:12px;\">Hasta:</label> <input type=\"date\" name=\"hasta\" value=\"" + (hasta.HasValue ? hasta.Value.ToString("yyyy-MM-dd") : "") + "\" />");
                sb.Append("<button type=\"submit\" class=\"btn-filtrar-reporte\" style=\"margin-left:12px;\">Filtrar</button></form>");
                if (!lista.Any())
                {
                    sb.Append("<p class=\"rep-placeholder\">No hay registros para las fechas seleccionadas.</p>");
                }
                else
                {
                    sb.Append("<table class=\"tabla-reporte\"><thead><tr><th>ID</th><th>Descripcion</th><th>Monto</th><th>Fecha</th><th>Estado</th></tr></thead><tbody>");
                    int i = 1;
                    foreach (var g in lista)
                    {
                        sb.Append("<tr><td>" + i++ + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(g.Descripcion) + "</td>");
                        sb.Append("<td>" + (g.Monto.HasValue ? g.Monto.Value.ToString("F2") : "-") + "</td>");
                        sb.Append("<td>" + (g.Fecha.HasValue ? g.Fecha.Value.ToString("dd/MM/yyyy") : "-") + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(g.NombreEstado) + "</td></tr>");
                    }
                    sb.Append("</tbody></table>");
                }
                sb.Append("</div></div></div></body></html>");
                return Content(sb.ToString(), "text/html");
            }
        }

        public ActionResult ReportesPacientesAtendidos(int page = 1, int pageSize = 20)
        {
            using (var contexto = new Contexto())
            {
                var hoy = DateTime.Today;
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

                int total = raw.Count;
                int totalPages = (int)Math.Ceiling(total / (double)pageSize);

                var lista = raw.OrderByDescending(r => r.Fecha).ThenByDescending(r => r.Hora)
                               .Skip((page - 1) * pageSize).Take(pageSize)
                               .Select(r => new CitaReporteDto
                               {
                                   IdCita = r.IdCita,
                                   Fecha = r.Fecha,
                                   HoraString = r.Hora.HasValue ? r.Hora.Value.ToString(@"hh\:mm") : "—",
                                   NombrePaciente = r.NombrePaciente,
                                   NombreDoctor = r.NombreDoctor,
                                   NombreMotivo = r.NombreMotivo,
                                   NombreEstado = r.NombreEstado
                               }).ToList();

                var sb = new System.Text.StringBuilder();
                sb.Append("<!DOCTYPE html><html lang=\"es\"><head><meta charset=\"utf-8\" /><title>Pacientes Atendidos</title>");
                sb.Append("<link href=\"/Content/DentalCare.css?v=3\" rel=\"stylesheet\" /><style>" + EstiloReporte + "</style></head><body>");
                sb.Append("<div class=\"page-wrapper\"><div class=\"reporte-page\">");
                sb.Append("<div class=\"reporte-header\"><div><h1>Pacientes Atendidos</h1></div>");
                sb.Append("<div><a href=\"/Reporteria\" class=\"btn-volver-reporte\">&larr; Volver</a></div></div>");
                sb.Append("<div class=\"reporte-card\">");
                if (!lista.Any())
                {
                    sb.Append("<p class=\"rep-placeholder\">No hay citas pasadas para mostrar.</p>");
                }
                else
                {
                    sb.Append("<table class=\"tabla-reporte\"><thead><tr><th>ID</th><th>Paciente</th><th>Doctor</th><th>Fecha</th><th>Hora</th><th>Motivo</th><th>Estado</th></tr></thead><tbody>");
                    int contador = ((page - 1) * pageSize) + 1;
                    foreach (var r in lista)
                    {
                        sb.Append("<tr><td>" + contador++ + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombrePaciente) + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombreDoctor) + "</td>");
                        sb.Append("<td>" + (r.Fecha.HasValue ? r.Fecha.Value.ToString("dd/MM/yyyy") : "-") + "</td>");
                        sb.Append("<td>" + r.HoraString + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombreMotivo) + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombreEstado) + "</td></tr>");
                    }
                    sb.Append("</tbody></table>");
                    sb.Append("<div class=\"rep-paginacion\">");
                    if (page > 1) sb.Append("<a href=\"/Reporteria/ReportesPacientesAtendidos?page=" + (page - 1) + "&pageSize=" + pageSize + "\">&larr; Anterior</a>");
                    sb.Append("<span>Pagina " + page + " de " + Math.Max(totalPages, 1) + "</span>");
                    if (page < totalPages) sb.Append("<a href=\"/Reporteria/ReportesPacientesAtendidos?page=" + (page + 1) + "&pageSize=" + pageSize + "\">Siguiente &rarr;</a>");
                    sb.Append("</div>");
                }
                sb.Append("</div></div></div></body></html>");
                return Content(sb.ToString(), "text/html");
            }
        }

        public ActionResult ReportesUsuarios()
        {
            var lista = _obtenerTodosLosUsuariosLN.Obtener();
            return View("~/Views/Usuario/ObtenerTodosLosUsuarios.cshtml", lista);
        }

        public ActionResult CitasCanceladasPorPeriodo(DateTime? desde, DateTime? hasta)
        {
            if (!desde.HasValue || !hasta.HasValue)
                return View(new List<CitaReporteDto>());

            if (desde.Value.Date > hasta.Value.Date)
            {
                TempData["Error"] = "El rango de fechas ingresado no es válido.";
                return View(new List<CitaReporteDto>());
            }

            var lista = _reporteCitasLN.ObtenerPorPeriodo(desde.Value, hasta.Value);
            var canceladas = lista.Where(c => c.FechaCancelacion.HasValue).ToList();
            return View("CitasCanceladas", canceladas);
        }

        public ActionResult ProductosBajoStock()
        {
            CargarCategoriasDropdown();
            return View(_reporteBajoStockLN.ObtenerProductosBajoStock());
        }

        [HttpPost]
        public ActionResult ProductosStockBajo(int? categoriaId)
        {
            var ln = new DentaCare.LogicaDeNegocio.Reporteria.Inventario.ReporteInventarioLN();
            var categoriasDto = ln.ObtenerCategorias() ?? new List<CategoriaDto>();
            ViewBag.Categorias = categoriasDto.Select(c => new SelectListItem { Value = c.IdCategoria.ToString(), Text = c.NombreCategoria }).ToList();

            if (categoriaId.HasValue)
            {
                bool exists = ((IEnumerable<SelectListItem>)ViewBag.Categorias).Any(x => x.Value == categoriaId.Value.ToString());
                if (!exists)
                {
                    TempData["Error"] = "Debe seleccionar una categoría válida.";
                    return View("StockBajo", new List<ProductoInventarioDto>());
                }
            }
            return View("StockBajo", ln.ObtenerProductosStockBajo(categoriaId));
        }

        public ActionResult ProductosPorVencer()
        {
            try { return View(_reporteProductosVencerLN.ObtenerProductosPorVencer(null, null)); }
            catch (Exception ex) { ViewBag.Error = ex.Message; return View(new List<ReporteProductosVencerDto>()); }
        }

        [HttpPost]
        public ActionResult ProductosPorVencer(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try { return View(_reporteProductosVencerLN.ObtenerProductosPorVencer(fechaInicio, fechaFin)); }
            catch (ArgumentException ex) { ViewBag.Error = ex.Message; return View(_reporteProductosVencerLN.ObtenerProductosPorVencer(null, null)); }
            catch (Exception ex) { ViewBag.Error = ex.Message; return View(new List<ReporteProductosVencerDto>()); }
        }

        public ActionResult CitasCanceladas()
        {
            try { return View(_reporteCitasCanceladasLN.ObtenerCitasCanceladas(null, null)); }
            catch (Exception ex) { ViewBag.Error = ex.Message; return View(new List<ReporteCitasCanceladasDto>()); }
        }

        [HttpPost]
        public ActionResult CitasCanceladas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try { return View(_reporteCitasCanceladasLN.ObtenerCitasCanceladas(fechaInicio, fechaFin)); }
            catch (ArgumentException ex) { ViewBag.Error = ex.Message; return View(_reporteCitasCanceladasLN.ObtenerCitasCanceladas(null, null)); }
            catch (Exception ex) { ViewBag.Error = ex.Message; return View(new List<ReporteCitasCanceladasDto>()); }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProcedimientosExpediente()
        {
            CargarExpedientesDropdown();
            return View(new List<ReporteProcedimientosDto>());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ProcedimientosExpediente(int idExpediente, DateTime? fechaInicio, DateTime? fechaFin)
        {
            CargarExpedientesDropdown(idExpediente);
            try
            {
                var modelo = fechaInicio.HasValue || fechaFin.HasValue
                    ? _reporteProcedimientosLN.ObtenerProcedimientosPorExpedienteFiltrado(idExpediente, fechaInicio, fechaFin)
                    : _reporteProcedimientosLN.ObtenerProcedimientosPorExpediente(idExpediente);
                // El trigger de FIDE_EXPEDIENTE_TB registra el acceso automáticamente
                return View(modelo);
            }
            catch (ArgumentException ex) { ViewBag.Error = ex.Message; return View(new List<ReporteProcedimientosDto>()); }
            catch (Exception ex) { ViewBag.Error = ex.Message; return View(new List<ReporteProcedimientosDto>()); }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Gastos(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio.Value.Date > fechaFin.Value.Date)
                {
                    ViewBag.Error = "El rango de fechas ingresado no es válido.";
                    return View(_reporteGastosLN.ObtenerGastos(null, null));
                }
                return View(_reporteGastosLN.ObtenerGastos(fechaInicio, fechaFin));
            }
            catch (ArgumentException ex) { ViewBag.Error = ex.Message; return View(_reporteGastosLN.ObtenerGastos(null, null)); }
            catch (Exception ex) { ViewBag.Error = ex.Message; return View(new List<ReporteGastosDto>()); }
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult HistorialDoctora(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                string aspNetUserId = User.Identity.GetUserId();
                var modelo = fechaInicio.HasValue || fechaFin.HasValue
                    ? _historialDoctoraLN.ObtenerHistorialPorDoctoraFiltrado(aspNetUserId, fechaInicio, fechaFin)
                    : _historialDoctoraLN.ObtenerHistorialPorDoctora(aspNetUserId);
                return View(modelo);
            }
            catch (ArgumentException ex) { ViewBag.Error = ex.Message; return View(_historialDoctoraLN.ObtenerHistorialPorDoctora(User.Identity.GetUserId())); }
            catch (Exception ex) { ViewBag.Error = ex.Message; return View(new List<HistorialDoctoraDto>()); }
        }

        public ActionResult Details(int id) => View();
        public ActionResult Create() => View();
        [HttpPost] public ActionResult Create(FormCollection c) { try { return RedirectToAction("Index"); } catch { return View(); } }
        public ActionResult Edit(int id) => View();
        [HttpPost] public ActionResult Edit(int id, FormCollection c) { try { return RedirectToAction("Index"); } catch { return View(); } }
        public ActionResult Delete(int id) => View();
        [HttpPost] public ActionResult Delete(int id, FormCollection c) { try { return RedirectToAction("Index"); } catch { return View(); } }

        private void CargarCategoriasDropdown(int? seleccionada = null)
        {
            using (var ctx = new Contexto())
            {
                var categorias = ctx.CategoriasProducto.Where(c => c.IdEstado == 1).Select(c => new { c.IdCategoria, c.NombreCategoria }).ToList();
                ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "NombreCategoria", seleccionada);
            }
        }

        private void CargarExpedientesDropdown(int? seleccionado = null)
        {
            var expedientes = _reporteProcedimientosLN.ObtenerExpedientes();
            ViewBag.Expedientes = new SelectList(expedientes, "IdExpediente", "NombrePaciente", seleccionado);
        }
    }
}