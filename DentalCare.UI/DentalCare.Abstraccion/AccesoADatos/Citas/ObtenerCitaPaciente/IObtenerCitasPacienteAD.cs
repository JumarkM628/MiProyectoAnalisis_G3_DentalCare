using DentalCare.Abstraccion.Modelo.Citas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerCitaPaciente
{
    public interface IObtenerCitasPacienteAD
    {
        List<CitaDto> ObtenerPorPaciente(string aspNetUserId);
        List<CitaDto> ObtenerCitasProximas24Horas();
    }
}
