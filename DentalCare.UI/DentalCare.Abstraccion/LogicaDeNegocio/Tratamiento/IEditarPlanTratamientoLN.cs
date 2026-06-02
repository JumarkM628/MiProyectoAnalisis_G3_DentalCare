using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Tratamiento;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Tratamiento
{
    public interface IEditarPlanTratamientoLN
    {
        PlanTratamientoDto ObtenerPlanPorId(int id);
        string EditarPlanTratamiento(PlanTratamientoDto dto, string nombreDoctora);
    }
}
