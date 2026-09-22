using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.AccesoADatos.Reporteria.Finanzas
{
    public interface IReporteGastosAD
    {
        List<ReporteGastosDto> ObtenerGastos(DateTime? fechaInicio, DateTime? fechaFin);
    }
}
