using DentalCare.Abstraccion.Modelo.Tratamientos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Tratamiento.RegistrarTratamiento
{
    public interface IRegistrarTratamientoLN
    {
        string Registrar(TratamientoDto dto);
    }
}
