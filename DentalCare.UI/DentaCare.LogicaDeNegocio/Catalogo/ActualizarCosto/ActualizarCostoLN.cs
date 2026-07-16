using DentalCare.Abstraccion.AccesoADatos.Catalogo.ActualizarCosto;
using DentalCare.Abstraccion.LogicaDeNegocio.Catalogo.ActualizarCosto;
using DentalCare.Abstraccion.Modelo.Catalogo;
using DentalCare.AccesoADatos.Catalogo.ActualizarCosto;

namespace DentalCare.LogicaDeNegocio.Catalogo.ActualizarCosto
{
    public class ActualizarCostoLN : IActualizarCostoLN
    {
        private readonly IActualizarCostoAD _actualizarAD;

        public ActualizarCostoLN()
        {
            _actualizarAD = new ActualizarCostoAD();
        }

        public string Actualizar(int idCatalogo, CatalogoDto dto)
        {
            if (dto.NuevoCosto <= 0)
                return "El nuevo costo debe ser mayor a cero.";

            _actualizarAD.Actualizar(idCatalogo, dto);
            return null;
        }
    }
}