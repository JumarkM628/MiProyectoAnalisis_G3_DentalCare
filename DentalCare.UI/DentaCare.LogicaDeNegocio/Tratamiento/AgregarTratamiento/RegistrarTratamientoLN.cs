using DentalCare.Abstraccion.AccesoADatos.Tratamiento.AgregarTratamiento;
using DentalCare.Abstraccion.LogicaDeNegocio.Tratamiento.RegistrarTratamiento;
using DentalCare.Abstraccion.Modelo.Tratamientos;
using DentalCare.AccesoADatos.Tratamientos.RegistrarTratamiento;

namespace DentalCare.LogicaDeNegocio.Tratamientos.RegistrarTratamiento
{
    public class RegistrarTratamientoLN : IRegistrarTratamientoLN
    {
        private readonly IRegistrarTratamientoAD _registrarAD;

        public RegistrarTratamientoLN()
        {
            _registrarAD = new RegistrarTratamientoAD();
        }

        public string Registrar(TratamientoDto dto)
        {
            // Verificar que la cita existe
            if (!_registrarAD.ExisteCita(dto.IdCitaForm))
                return "No se encontró la cita indicada.";

            _registrarAD.Registrar(dto);
            return null;
        }
    }
}