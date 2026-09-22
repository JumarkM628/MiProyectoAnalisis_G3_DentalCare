using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Reporteria;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Reporteria.Usuario
{
    public interface IHistorialDoctoraLN
    {
        List<HistorialDoctoraDto> ObtenerHistorialPorDoctora(string aspNetUserId);

        List<HistorialDoctoraDto> ObtenerHistorialPorDoctoraFiltrado(
            string aspNetUserId, DateTime? fechaInicio, DateTime? fechaFin);
    }
}