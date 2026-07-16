using DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerCitaPaciente;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerCitasPaciente;
using DentalCare.Abstraccion.Modelo.Citas;
using DentalCare.AccesoADatos.Citas.ObtenerCitaPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Citas.ObtenerCitasPaciente
{
    public class ObtenerCitasPacienteLN : IObtenerCitasPacienteLN
    {
        private readonly IObtenerCitasPacienteAD _obtenerAD;

        public ObtenerCitasPacienteLN()
        {
            _obtenerAD = new ObtenerCitasPacienteAD();
        }

        public List<CitaDto> ObtenerPorPaciente(string aspNetUserId)
        {
            return _obtenerAD.ObtenerPorPaciente(aspNetUserId);
        }
    }
}
