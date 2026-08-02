using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Finanzas;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.AccesoADatos.Reporteria.Finanzas
{
    public class ReporteGastosAD : IReporteGastosAD
    {
        private readonly Contexto _contexto;

        public ReporteGastosAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public List<ReporteGastosDto> ObtenerGastos(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var consulta =
                from gasto in _contexto.Gastos

                join estado in _contexto.Estados
                    on gasto.IdEstado equals estado.IdEstado
                    into estadoJoin
                from estado in estadoJoin.DefaultIfEmpty()

                where (!fechaInicio.HasValue || gasto.Fecha >= fechaInicio.Value)
                   && (!fechaFin.HasValue || gasto.Fecha <= fechaFin.Value)

                orderby gasto.Fecha descending

                select new ReporteGastosDto
                {
                    IdGasto = gasto.IdGasto,
                    Descripcion = gasto.Descripcion,
                    Monto = gasto.Monto,
                    Fecha = gasto.Fecha,
                    Estado = estado == null ? "—" : estado.NombreEstado
                };

            return consulta.ToList();
        }
    }
}
