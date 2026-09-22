using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Tratamiento;

namespace DentalCare.Abstraccion.AccesoADatos.Tratamiento
{
    public interface IEditarPlanTratamientoAD
    {
        PlanTratamientoDto ObtenerPlanPorId(int id);
        bool GuardarCambios(PlanTratamientoDto dto);
    }
}
