using DentalCare.Abstraccion.Modelo.Tratamientos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Tratamiento.AgregarTratamiento
{
    public interface IRegistrarTratamientoAD
    {
        void Registrar(TratamientoDto dto);
        bool ExisteCita(int idCita);
    }
}
