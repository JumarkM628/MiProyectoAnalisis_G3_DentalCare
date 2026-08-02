using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Producto
{
    public interface IReporteBajoStockLN
    {
        List<ReporteBajoStockDto> ObtenerProductosBajoStock();

        List<ReporteBajoStockDto> ObtenerProductosBajoStockPorCategoria(int idCategoria);
    }
}
