using DentalCare.Abstraccion.Modelo.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Catalogo.ActualizarCosto
{
    public interface IActualizarCostoAD
    {
        void Actualizar(int idCatalogo, CatalogoDto dto);

    }
}
