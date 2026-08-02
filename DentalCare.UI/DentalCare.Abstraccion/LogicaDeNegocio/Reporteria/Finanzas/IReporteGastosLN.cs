using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Finanzas
{
    public interface IReporteGastosLN
    {
        List<ReporteGastosDto> ObtenerGastos(DateTime? fechaInicio, DateTime? fechaFin);
    }
}