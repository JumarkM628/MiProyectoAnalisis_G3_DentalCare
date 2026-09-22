using DentalCare.Abstraccion.Modelo.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Bitacora
{
    public interface IObtenerEventosAD
    {
        List<EventoDto> Obtener();
    }
}
