using DentalCare.Abstraccion.Modelo.Gasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Gastos
{
    public interface IRegistrarGastoLN
    {
        string Registrar(GastoDto dto);
    }
}
