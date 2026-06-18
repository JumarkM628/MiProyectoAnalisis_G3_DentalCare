using DentalCare.Abstraccion.AccesoADatos.Citas.ObtenerTodasLasCitas;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.ObtenerTodasLasCitas;
using DentalCare.Abstraccion.Modelo.Citas;
using DentalCare.AccesoADatos.Citas.ObtenerTodasLasCitas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Citas.ObtenerTodasLasCitas
{
    public class ObtenerTodasLasCitasLN : IObtenerTodasLasCitasLN
    {
        private readonly IObtenerTodasLasCitasAD _obtenerAD;

        public ObtenerTodasLasCitasLN()
        {
            _obtenerAD = new ObtenerTodasLasCitasAD();
        }

        public List<CitaDto> Obtener()
        {
            return _obtenerAD.Obtener()
                .OrderBy(c => c.Fecha)
                .ThenBy(c => c.Hora)
                .ToList();
        }
    }
}
