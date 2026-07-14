using DentalCare.Abstraccion.Modelo.Gasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Gastos
{
    public interface IRegistrarGastoAD
    {
        void Registrar(GastoDto dto);
    }
}
