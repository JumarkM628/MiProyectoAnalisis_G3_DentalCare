using DentalCare.Abstraccion.Modelo.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Citas.AgregarCita
{
    public interface IAgregarCitaAD
    {
        void Agregar(CitaDto dto, int idPaciente);
        bool ExistePaciente(string cedulaPaciente);
        bool HorarioOcupado(int idDoctor, DateTime fecha, TimeSpan hora);
    }
}
