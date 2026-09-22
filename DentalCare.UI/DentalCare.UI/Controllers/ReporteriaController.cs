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

        // ─────────────────────────────────────────────────────────────
        // Estilos de los reportes armados con StringBuilder.
        // Son las mismas clases y los mismos tokens que usan las vistas
        // .cshtml del resto del sistema, para que todo se vea igual.
        // ─────────────────────────────────────────────────────────────
        private const string EstiloReporte =
            ".inv-page{padding:36px 20px 56px;" +
                "--dc-teal-oscuro:#0f3b3c;--dc-teal:#2e8f8c;--dc-teal-barra:#1c6a68;" +
                "--dc-tinta:#1e2d3d;--dc-suave:#6b7f92;--dc-input-bg:#f5f8fe;--dc-input-borde:#dde5f3;" +
                "--dc-verde:#2f6d5c;--dc-verde-hover:#275c4d;--dc-gris:#e9ebed;--dc-gris-hover:#dde1e5;" +
                "--dc-error:#c0392b;--dc-ancho:1100px;}" +

            ".inv-header{max-width:var(--dc-ancho);margin:0 auto 20px;padding:28px 34px;border-radius:16px;" +
                "background:linear-gradient(135deg,var(--dc-teal-oscuro) 0%,var(--dc-teal) 100%);" +
                "box-shadow:0 10px 26px rgba(15,59,60,.18);}" +
            ".exp-header{display:flex;align-items:center;justify-content:space-between;gap:20px;flex-wrap:wrap;}" +
            ".inv-titulo{margin:0;color:#fff;font-size:1.55rem;font-weight:800;letter-spacing:-.01em;}" +
            ".inv-subtitulo{margin:10px 0 0;color:rgba(255,255,255,.82);font-size:.95rem;font-weight:500;}" +

            ".btn-header{display:inline-flex;align-items:center;height:40px;padding:0 20px;border-radius:8px;" +
                "border:1px solid rgba(255,255,255,.45);background:rgba(255,255,255,.12);color:#fff;" +
                "font-size:.88rem;font-weight:600;text-decoration:none;white-space:nowrap;" +
                "transition:background .18s ease,border-color .18s ease;}" +
            ".btn-header:hover,.btn-header:focus{background:rgba(255,255,255,.22);border-color:#fff;color:#fff;text-decoration:none;}" +

            ".trat-form-card{max-width:var(--dc-ancho);margin:0 auto 22px;padding:0;overflow:hidden;" +
                "border-radius:16px;background:#fff;box-shadow:0 10px 26px rgba(15,59,60,.09);}" +
            ".trat-card-bar{padding:18px 30px;color:#fff;font-size:1.02rem;font-weight:700;" +
                "background:linear-gradient(135deg,var(--dc-teal-oscuro) 0%,var(--dc-teal-barra) 100%);}" +
            ".trat-card-body{padding:24px 30px 28px;}" +

            ".filtro-barra{display:flex;align-items:flex-end;gap:14px;flex-wrap:wrap;" +
                "margin-bottom:20px;padding-bottom:20px;border-bottom:1px solid #eef2f7;}" +
            ".filtro-campo{display:flex;flex-direction:column;gap:7px;}" +
            ".trat-label{margin:0;font-size:.82rem;font-weight:700;color:var(--dc-tinta);}" +
            ".trat-input{box-sizing:border-box;height:44px;padding:10px 14px;border-radius:8px;" +
                "border:1px solid var(--dc-input-borde);background-color:var(--dc-input-bg);" +
                "font-family:inherit;font-size:.92rem;color:var(--dc-tinta);outline:none;" +
                "transition:border-color .18s ease,box-shadow .18s ease,background-color .18s ease;}" +
            ".trat-input:hover{border-color:#c8d6ec;}" +
            ".trat-input:focus{background-color:#fff;border-color:var(--dc-teal);box-shadow:0 0 0 3px rgba(46,143,140,.15);}" +
            ".trat-input::-webkit-calendar-picker-indicator{opacity:.5;cursor:pointer;}" +

            ".contador-pill{margin-left:auto;padding:8px 16px;border-radius:999px;" +
                "background:rgba(46,143,140,.1);color:#216d6a;font-size:.8rem;font-weight:700;white-space:nowrap;}" +

            ".tabla-scroll{overflow-x:auto;border:1px solid var(--dc-input-borde);border-radius:12px;}" +
            ".rep-tabla{width:100%;border-collapse:collapse;font-size:.9rem;}" +
            ".rep-tabla thead th{padding:12px 16px;background:#eef4f9;border-bottom:1px solid var(--dc-input-borde);" +
                "font-size:.72rem;font-weight:800;letter-spacing:.07em;text-transform:uppercase;" +
                "color:var(--dc-suave);text-align:left;white-space:nowrap;}" +
            ".rep-tabla td{padding:12px 16px;border-bottom:1px solid #f0f4f8;color:var(--dc-tinta);vertical-align:middle;}" +
            ".rep-tabla tbody tr:last-child td{border-bottom:none;}" +
            ".rep-tabla tbody tr:hover td{background:#f8fbfe;}" +
            ".celda-num{width:60px;color:var(--dc-suave);font-size:.85rem;}" +
            ".celda-desc{font-weight:600;min-width:220px;}" +
            ".celda-monto{text-align:right;font-variant-numeric:tabular-nums;white-space:nowrap;}" +
            ".col-monto{text-align:right;}" +

            ".rep-badge{display:inline-block;padding:4px 13px;border-radius:999px;" +
                "background:rgba(46,143,140,.1);color:#216d6a;font-size:.76rem;font-weight:700;white-space:nowrap;}" +

            ".estado-vacio{padding:40px 20px;text-align:center;border:1px dashed var(--dc-input-borde);" +
                "border-radius:12px;background:var(--dc-input-bg);}" +
            ".estado-vacio-titulo{margin:0 0 6px;font-size:1rem;font-weight:700;color:var(--dc-tinta);}" +
            ".estado-vacio-texto{margin:0 auto;max-width:420px;font-size:.88rem;line-height:1.5;color:var(--dc-suave);}" +

            ".btn-inv-primary{height:44px;padding:0 24px;border:none;border-radius:8px;background:var(--dc-verde);" +
                "color:#fff;font-family:inherit;font-size:.9rem;font-weight:700;cursor:pointer;" +
                "transition:background .18s ease,box-shadow .18s ease;}" +
            ".btn-inv-primary:hover{background:var(--dc-verde-hover);color:#fff;box-shadow:0 6px 14px rgba(47,109,92,.28);}" +
            ".btn-inv-cancelar{display:inline-flex;align-items:center;height:44px;padding:0 22px;border-radius:8px;" +
                "background:var(--dc-gris);color:#5a6672;font-size:.9rem;font-weight:600;text-decoration:none;" +
                "transition:background .18s ease,color .18s ease;}" +
            ".btn-inv-cancelar:hover{background:var(--dc-gris-hover);color:var(--dc-tinta);text-decoration:none;}" +

            ".rep-paginacion{display:flex;align-items:center;justify-content:center;gap:14px;margin-top:20px;}" +
            ".rep-paginacion a{display:inline-flex;align-items:center;height:40px;padding:0 18px;border-radius:8px;" +
                "background:var(--dc-input-bg);border:1px solid var(--dc-input-borde);color:var(--dc-tinta);" +
                "font-size:.86rem;font-weight:600;text-decoration:none;transition:background .18s ease;}" +
            ".rep-paginacion a:hover{background:#e6eef8;color:var(--dc-tinta);text-decoration:none;}" +
            ".rep-paginacion span{color:var(--dc-suave);font-size:.84rem;font-weight:700;}" +

            "@media(max-width:680px){" +
                ".inv-page{padding:22px 14px 40px;}" +
                ".inv-header{padding:22px;}" +
                ".trat-card-bar{padding:16px 20px;}" +
                ".trat-card-body{padding:20px 20px 22px;}" +
                ".filtro-campo{flex:1 1 100%;}" +
                ".trat-input{width:100%;}" +
                ".contador-pill{margin-left:0;}" +
            "}";

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

        // ─────────────────────────────────────────────────────────────
        // Ayudantes de presentación de los reportes con StringBuilder.
        // Solo arman HTML: no consultan nada ni cambian comportamiento.
        // ─────────────────────────────────────────────────────────────

        private static string AbrirReporte(string titulo, string subtitulo, string tituloTarjeta)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("<style>" + EstiloReporte + "</style>");
            sb.Append("<div class=\"inv-page\">");
            sb.Append("<div class=\"inv-header exp-header\">");
            sb.Append("<div><h1 class=\"inv-titulo\">" + System.Net.WebUtility.HtmlEncode(titulo) + "</h1>");
            if (!string.IsNullOrEmpty(subtitulo))
            {
                sb.Append("<p class=\"inv-subtitulo\">" + System.Net.WebUtility.HtmlEncode(subtitulo) + "</p>");
            }
            sb.Append("</div>");
            sb.Append("<a href=\"/Reporteria\" class=\"btn-header\">Volver</a>");
            sb.Append("</div>");
            sb.Append("<div class=\"trat-form-card\">");
            sb.Append("<div class=\"trat-card-bar\">" + System.Net.WebUtility.HtmlEncode(tituloTarjeta) + "</div>");
            sb.Append("<div class=\"trat-card-body\">");
            return sb.ToString();
        }

        private static string CerrarReporte()
        {
            return "</div></div></div>";
        }

        private static string FiltroFechas(string accion, DateTime? desde, DateTime? hasta, int cantidad, string etiquetaCantidad)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("<form method=\"get\" action=\"/Reporteria/" + accion + "\" class=\"filtro-barra\">");
            sb.Append("<div class=\"filtro-campo\"><label class=\"trat-label\" for=\"desde\">Desde</label>");
            sb.Append("<input type=\"date\" id=\"desde\" name=\"desde\" class=\"trat-input\" value=\"" +
                      (desde.HasValue ? desde.Value.ToString("yyyy-MM-dd") : "") + "\" /></div>");
            sb.Append("<div class=\"filtro-campo\"><label class=\"trat-label\" for=\"hasta\">Hasta</label>");
            sb.Append("<input type=\"date\" id=\"hasta\" name=\"hasta\" class=\"trat-input\" value=\"" +
                      (hasta.HasValue ? hasta.Value.ToString("yyyy-MM-dd") : "") + "\" /></div>");
            sb.Append("<button type=\"submit\" class=\"btn-inv-primary\">Filtrar</button>");
            if (desde.HasValue || hasta.HasValue)
            {
                sb.Append("<a href=\"/Reporteria/" + accion + "\" class=\"btn-inv-cancelar\">Limpiar</a>");
            }
            sb.Append("<span class=\"contador-pill\">" + cantidad + " " + etiquetaCantidad + "</span>");
            sb.Append("</form>");
            return sb.ToString();
        }

        private static string EstadoVacio(string titulo, string texto)
        {
            return "<div class=\"estado-vacio\"><p class=\"estado-vacio-titulo\">" +
                   System.Net.WebUtility.HtmlEncode(titulo) + "</p><p class=\"estado-vacio-texto\">" +
                   System.Net.WebUtility.HtmlEncode(texto) + "</p></div>";
        }

        // Devuelve el HTML ya armado a traves de la vista puente
        // Views/Shared/ReporteHtml.cshtml para que pase por _Layout.
        // El cast a object es obligatorio: sin el, C# elige la sobrecarga
        // View(string viewName, string masterName) y la pagina sale vacia.
        private ActionResult VistaReporte(string titulo, System.Text.StringBuilder sb)
        {
            ViewBag.Title = titulo;
            return View("ReporteHtml", (object)sb.ToString());
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
                sb.Append(AbrirReporte("Reportes de Tratamientos",
                                       "Planes de tratamiento registrados en la clinica.",
                                       "Tratamientos"));
                sb.Append(FiltroFechas("ReportesTratamientos", desde, hasta, raw.Count, "tratamientos"));

                if (!raw.Any())
                {
                    sb.Append(EstadoVacio("Sin resultados",
                        desde.HasValue || hasta.HasValue
                            ? "No hay tratamientos en el rango de fechas seleccionado."
                            : "Todavia no hay planes de tratamiento registrados."));
                }
                else
                {
                    sb.Append("<div class=\"tabla-scroll\"><table class=\"rep-tabla\"><thead><tr>");
                    sb.Append("<th>#</th><th>Descripcion</th><th>Inicio</th><th>Fin</th><th>Estado</th>");
                    sb.Append("<th class=\"col-monto\">Monto</th><th>Cita</th>");
                    sb.Append("</tr></thead><tbody>");
                    int i = 1;
                    foreach (var item in raw)
                    {
                        var t = item.Tratamiento;
                        sb.Append("<tr>");
                        sb.Append("<td class=\"celda-num\">" + i++ + "</td>");
                        sb.Append("<td class=\"celda-desc\">" + System.Net.WebUtility.HtmlEncode(t.Descripcion) + "</td>");
                        sb.Append("<td>" + (t.FechaInicio.HasValue ? t.FechaInicio.Value.ToString("dd/MM/yyyy") : "&mdash;") + "</td>");
                        sb.Append("<td>" + (t.FechaFin.HasValue ? t.FechaFin.Value.ToString("dd/MM/yyyy") : "&mdash;") + "</td>");
                        sb.Append("<td><span class=\"rep-badge\">" + System.Net.WebUtility.HtmlEncode(item.NombreEstado) + "</span></td>");
                        sb.Append("<td class=\"celda-monto\">" + (t.Monto.HasValue ? "&#8353; " + t.Monto.Value.ToString("N2") : "&mdash;") + "</td>");
                        sb.Append("<td class=\"celda-num\">" + (t.IdCita.HasValue ? t.IdCita.ToString() : "&mdash;") + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody></table></div>");
                }

                sb.Append(CerrarReporte());
                return VistaReporte("Reportes de Tratamientos", sb);
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
                sb.Append(AbrirReporte("Reporte de Productos Utilizados",
                                       "Consumo de productos por procedimiento.",
                                       "Productos utilizados"));

                if (!raw.Any())
                {
                    sb.Append(EstadoVacio("Sin resultados", "Todavia no hay consumo de productos registrado."));
                }
                else
                {
                    sb.Append("<div class=\"tabla-scroll\"><table class=\"rep-tabla\"><thead><tr>");
                    sb.Append("<th>#</th><th>Producto</th><th>Procedimiento</th><th>Cantidad</th><th>Estado</th>");
                    sb.Append("</tr></thead><tbody>");
                    int i = 1;
                    foreach (var item in raw)
                    {
                        sb.Append("<tr><td class=\"celda-num\">" + i++ + "</td>");
                        sb.Append("<td class=\"celda-desc\">" + System.Net.WebUtility.HtmlEncode(item.NombreProducto) + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(item.ProcedimientoDescripcion) + "</td>");
                        sb.Append("<td>" + (item.Uso.CANTIDAD.HasValue ? item.Uso.CANTIDAD.Value.ToString() : "&mdash;") + "</td>");
                        sb.Append("<td><span class=\"rep-badge\">" + System.Net.WebUtility.HtmlEncode(item.NombreEstado) + "</span></td></tr>");
                    }
                    sb.Append("</tbody></table></div>");
                }

                sb.Append(CerrarReporte());
                return VistaReporte("Productos Utilizados", sb);
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
                sb.Append(AbrirReporte("Reportes de Pagos",
                                       "Gastos registrados en el periodo seleccionado.",
                                       "Pagos"));
                sb.Append(FiltroFechas("ReportesPagos", desde, hasta, lista.Count, "registros"));

                if (!lista.Any())
                {
                    sb.Append(EstadoVacio("Sin resultados",
                        desde.HasValue || hasta.HasValue
                            ? "No hay pagos en el rango de fechas seleccionado."
                            : "Todavia no hay pagos registrados."));
                }
                else
                {
                    sb.Append("<div class=\"tabla-scroll\"><table class=\"rep-tabla\"><thead><tr>");
                    sb.Append("<th>#</th><th>Descripcion</th><th class=\"col-monto\">Monto</th><th>Fecha</th><th>Estado</th>");
                    sb.Append("</tr></thead><tbody>");
                    int i = 1;
                    foreach (var g in lista)
                    {
                        sb.Append("<tr><td class=\"celda-num\">" + i++ + "</td>");
                        sb.Append("<td class=\"celda-desc\">" + System.Net.WebUtility.HtmlEncode(g.Descripcion) + "</td>");
                        sb.Append("<td class=\"celda-monto\">" + (g.Monto.HasValue ? "&#8353; " + g.Monto.Value.ToString("N2") : "&mdash;") + "</td>");
                        sb.Append("<td>" + (g.Fecha.HasValue ? g.Fecha.Value.ToString("dd/MM/yyyy") : "&mdash;") + "</td>");
                        sb.Append("<td><span class=\"rep-badge\">" + System.Net.WebUtility.HtmlEncode(g.NombreEstado) + "</span></td></tr>");
                    }
                    sb.Append("</tbody></table></div>");
                }

                sb.Append(CerrarReporte());
                return VistaReporte("Reportes de Pagos", sb);
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
                sb.Append(AbrirReporte("Pacientes Atendidos",
                                       "Citas anteriores a la fecha de hoy.",
                                       "Pacientes atendidos"));

                if (!lista.Any())
                {
                    sb.Append(EstadoVacio("Sin resultados", "No hay citas pasadas para mostrar."));
                }
                else
                {
                    sb.Append("<div class=\"tabla-scroll\"><table class=\"rep-tabla\"><thead><tr>");
                    sb.Append("<th>#</th><th>Paciente</th><th>Doctor</th><th>Fecha</th><th>Hora</th><th>Motivo</th><th>Estado</th>");
                    sb.Append("</tr></thead><tbody>");
                    int contador = ((page - 1) * pageSize) + 1;
                    foreach (var r in lista)
                    {
                        sb.Append("<tr><td class=\"celda-num\">" + contador++ + "</td>");
                        sb.Append("<td class=\"celda-desc\">" + System.Net.WebUtility.HtmlEncode(r.NombrePaciente) + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombreDoctor) + "</td>");
                        sb.Append("<td>" + (r.Fecha.HasValue ? r.Fecha.Value.ToString("dd/MM/yyyy") : "&mdash;") + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.HoraString) + "</td>");
                        sb.Append("<td>" + System.Net.WebUtility.HtmlEncode(r.NombreMotivo) + "</td>");
                        sb.Append("<td><span class=\"rep-badge\">" + System.Net.WebUtility.HtmlEncode(r.NombreEstado) + "</span></td></tr>");
                    }
                    sb.Append("</tbody></table></div>");

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

                sb.Append(CerrarReporte());
                return VistaReporte("Pacientes Atendidos", sb);
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
