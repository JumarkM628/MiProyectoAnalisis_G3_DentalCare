using System;
using System.Collections.Generic;
using DentalCare.Abstraccion.Modelo.Reporteria;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Reporteria.Inventario;

namespace DentaCare.LogicaDeNegocio.Reporteria.Inventario
{
    public class ReporteInventarioLN
    {
        private readonly ReporteInventarioAD _ad;

        public ReporteInventarioLN()
        {
            _ad = new ReporteInventarioAD(new Contexto());
        }

        // Optional constructor for tests or DI
        public ReporteInventarioLN(ReporteInventarioAD reporteAd)
        {
            _ad = reporteAd ?? new ReporteInventarioAD(new Contexto());
        }

        public List<CategoriaDto> ObtenerCategorias()
        {
            return _ad.ObtenerCategoriasDto();
        }

        public List<ProductoInventarioDto> ObtenerProductosStockBajo(int? categoriaId = null)
        {
            return _ad.ObtenerProductosStockBajo(categoriaId);
        }

        public List<ReporteProductosVencerDto> ObtenerProductosPorVencer(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Default to next 30 days when both are null
            if (!fechaInicio.HasValue && !fechaFin.HasValue)
            {
                var inicio = DateTime.Today;
                var fin = DateTime.Today.AddDays(30);
                return _ad.ObtenerProductosPorVencer(inicio, fin);
            }

            // Require both dates or throw to let controller handle
            if (!fechaInicio.HasValue || !fechaFin.HasValue)
                throw new ArgumentException("Debe ingresar ambas fechas.");

            if (fechaInicio.Value.Date > fechaFin.Value.Date)
                throw new ArgumentException("El rango de fechas ingresado no es válido.");

            return _ad.ObtenerProductosPorVencer(fechaInicio.Value.Date, fechaFin.Value.Date);
        }
    }
}
