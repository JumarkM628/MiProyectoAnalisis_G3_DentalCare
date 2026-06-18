using DentalCare.Abstraccion.Modelo.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerTodasLasCitas
{
    public interface IObtenerTodasLasCitasLN
    {
        List<CitaDto> Obtener();
    }
}
