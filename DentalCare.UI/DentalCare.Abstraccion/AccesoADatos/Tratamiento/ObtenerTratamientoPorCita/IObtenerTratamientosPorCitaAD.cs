using DentalCare.Abstraccion.Modelo.Tratamientos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Tratamiento.Tratamientos
{
    public interface IObtenerTratamientosPorCitaAD
    {
        List<TratamientoDto> Obtener(int idCita);

    }
}
