using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.AccesoADatos.Reporteria.Producto
{
    public interface IReporteProductosVencerAD
    {
        List<ReporteProductosVencerDto> ObtenerProductosPorVencer(DateTime? fechaInicio, DateTime? fechaFin);
    }
}
