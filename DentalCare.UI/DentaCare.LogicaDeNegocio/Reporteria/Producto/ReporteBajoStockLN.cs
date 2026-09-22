using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Producto
{
    public class ReporteBajoStockLN : IReporteBajoStockLN
    {
        private readonly IReporteBajoStockAD _reporteBajoStockAD;
        public ReporteBajoStockLN(IReporteBajoStockAD reporteBajoStockAD)
        {
            _reporteBajoStockAD = reporteBajoStockAD;
        }
        public List<ReporteBajoStockDto> ObtenerProductosBajoStock()
        {
            return _reporteBajoStockAD.ObtenerProductosBajoStock();
        }
        public List<ReporteBajoStockDto> ObtenerProductosBajoStockPorCategoria(int idCategoria)
        {
            if (idCategoria <= 0)
                throw new Exception("Debe seleccionar una categoría válida.");

            return _reporteBajoStockAD.ObtenerProductosBajoStockPorCategoria(idCategoria);
        }
    }
}