using DentalCare.Abstraccion.AccesoADatos.Odontograma;
using DentalCare.Abstraccion.AccesoADatos.Odontograma.RegistrarOdontograma;
using DentalCare.Abstraccion.LogicaDeNegocio.Odontograma.RegistrarOdontograma;
using DentalCare.Abstraccion.Modelo.Odontograma;
using DentalCare.AccesoADatos.Odontograma.RegistrarOdontograma;

namespace DentalCare.LogicaDeNegocio.Odontograma.RegistrarOdontograma
{
    public class RegistrarOdontogramaLN : IRegistrarOdontogramaLN
    {
        private readonly IRegistrarOdontogramaAD _registrarAD;

        public RegistrarOdontogramaLN()
        {
            _registrarAD = new RegistrarOdontogramaAD();
        }

        public string Registrar(OdontogramaDto dto)
        {
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                return "Debe seleccionar al menos una pieza dental con su estado.";

            foreach (var detalle in dto.Detalles)
            {
                if (string.IsNullOrEmpty(detalle.EstadoPieza))
                    return "Todas las piezas seleccionadas deben tener un estado asignado.";
            }

            _registrarAD.Registrar(dto);
            return null;
        }
    }
}