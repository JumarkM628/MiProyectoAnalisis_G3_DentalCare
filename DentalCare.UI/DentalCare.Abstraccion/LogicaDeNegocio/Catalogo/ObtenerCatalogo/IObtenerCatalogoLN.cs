using DentalCare.Abstraccion.Modelo.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Catalogo.ObtenerCatalogo
{
    public interface IObtenerCatalogoLN
    {
        List<CatalogoDto> Obtener();
        CatalogoDto ObtenerPorId(int idCatalogo);
    }
}
