using DentalCare.Abstraccion.Modelo.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerTodasLasCitas
{
    public interface IObtenerTodasLasCitasAD
    {
        List<CitaDto> Obtener();
    }
}
