using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Reporteria.Usuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Usuario;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentaCare.LogicaDeNegocio.Reporteria.Usuario
{
    public class HistorialDoctoraLN : IHistorialDoctoraLN
    {
        private readonly IHistorialDoctoraAD _historialDoctoraAD;

        public HistorialDoctoraLN(IHistorialDoctoraAD historialDoctoraAD)
        {
            _historialDoctoraAD = historialDoctoraAD;
        }
        public List<HistorialDoctoraDto> ObtenerHistorialPorDoctora(string aspNetUserId)
        {
            if (string.IsNullOrEmpty(aspNetUserId))
                throw new ArgumentException("No se pudo identificar al usuario autenticado.");

            return _historialDoctoraAD.ObtenerHistorialPorDoctora(aspNetUserId);
        }
        public List<HistorialDoctoraDto> ObtenerHistorialPorDoctoraFiltrado(
            string aspNetUserId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (string.IsNullOrEmpty(aspNetUserId))
                throw new ArgumentException("No se pudo identificar al usuario autenticado.");
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
                throw new ArgumentException(
                    "La fecha inicial no puede ser mayor que la fecha final.");

            return _historialDoctoraAD.ObtenerHistorialPorDoctoraFiltrado(
                aspNetUserId, fechaInicio, fechaFin);
        }
    }
}
