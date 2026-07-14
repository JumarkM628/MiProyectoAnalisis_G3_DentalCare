using DentalCare.Abstraccion.AccesoADatos.Catalogo.ObtenerCatalogoAD;
using DentalCare.Abstraccion.LogicaDeNegocio.Catalogo.ObtenerCatalogo;
using DentalCare.Abstraccion.Modelo.Catalogo;
using DentalCare.AccesoADatos.Catalogo.ObtenerCatalogo;
using System.Collections.Generic;

namespace DentalCare.LogicaDeNegocio.Catalogo.ObtenerCatalogo
{
    public class ObtenerCatalogoLN : IObtenerCatalogoLN
    {
        private readonly IObtenerCatalogoAD _obtenerAD;

        public ObtenerCatalogoLN()
        {
            _obtenerAD = new ObtenerCatalogoAD();
        }

        public List<CatalogoDto> Obtener() => _obtenerAD.Obtener();
        public CatalogoDto ObtenerPorId(int idCatalogo) => _obtenerAD.ObtenerPorId(idCatalogo);
    }
}