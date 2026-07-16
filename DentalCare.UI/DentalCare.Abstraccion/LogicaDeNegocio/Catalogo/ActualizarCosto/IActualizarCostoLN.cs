using DentalCare.Abstraccion.Modelo.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Catalogo.ActualizarCosto
{
    public interface IActualizarCostoLN
    {
        string Actualizar(int idCatalogo, CatalogoDto dto);
    }
}
