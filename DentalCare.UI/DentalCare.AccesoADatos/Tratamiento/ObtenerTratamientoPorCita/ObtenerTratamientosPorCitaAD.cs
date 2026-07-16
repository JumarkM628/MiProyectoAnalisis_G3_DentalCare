
using DentalCare.Abstraccion.AccesoADatos.Tratamiento.Tratamientos;
using DentalCare.Abstraccion.Modelo.Tratamientos;
using System.Collections.Generic;
using System.Linq;

namespace DentalCare.AccesoADatos.Tratamientos.ObtenerTratamientosPorCita
{
    public class ObtenerTratamientosPorCitaAD : IObtenerTratamientosPorCitaAD
    {
        private readonly Contexto _contexto;

        public ObtenerTratamientosPorCitaAD()
        {
            _contexto = new Contexto();
        }

        /// <summary>
        /// Escenario 5: obtiene todos los tratamientos de una cita
        /// con el total acumulado calculado (Escenario 4).
        /// </summary>
        public List<TratamientoDto> Obtener(int idCita)
        {
            var rawData = (
                from t in _contexto.PlanesTratamiento
                where t.IdCita == idCita

                join estado in _contexto.Estados
                    on t.IdEstado equals estado.IdEstado

                select new
                {
                    t.IdTratamiento,
                    t.IdCita,
                    t.Descripcion,
                    t.Monto,
                    t.FechaInicio,
                    t.FechaFin,
                    t.IdEstado,
                    NombreEstado = estado.NombreEstado
                }
            ).ToList();

            // Escenario 4: calcular total acumulado
            decimal totalCita = rawData.Sum(t => t.Monto ?? 0);

            return rawData.Select(t => new TratamientoDto
            {
                IdTratamiento = t.IdTratamiento,
                IdCita = t.IdCita ?? 0,
                DescripcionMostrar = t.Descripcion,
                MontoMostrar = t.Monto ?? 0,
                FechaInicio = t.FechaInicio,
                FechaFin = t.FechaFin,
                NombreEstado = t.NombreEstado,
                TotalCita = totalCita  // mismo total para todas las filas
            }).ToList();
        }
    }
}