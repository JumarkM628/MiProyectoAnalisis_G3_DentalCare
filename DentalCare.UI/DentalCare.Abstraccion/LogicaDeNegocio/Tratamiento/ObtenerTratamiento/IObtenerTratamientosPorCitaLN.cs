using DentalCare.Abstraccion.Modelo.Tratamientos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Tratamiento.ObtenerTratamientoLN
{
    public interface IObtenerTratamientosPorCitaLN
    {
        List<TratamientoDto> Obtener(int idCita);
    }
}
