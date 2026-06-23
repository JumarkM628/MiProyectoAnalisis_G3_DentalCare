using DentalCare.Abstraccion.Modelo.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerCitasPaciente
{
    public interface IObtenerCitasPacienteLN
    {
        List<CitaDto> ObtenerPorPaciente(string aspNetUserId);
    }
}
