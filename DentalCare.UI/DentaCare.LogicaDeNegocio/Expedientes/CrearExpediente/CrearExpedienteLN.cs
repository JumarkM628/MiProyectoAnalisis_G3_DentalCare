using DentalCare.Abstraccion.AccesoADatos.Expediente.CrearExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Expedientes.CrearExpediente;

namespace DentalCare.LogicaDeNegocio.Expedientes.CrearExpediente
{
    public class CrearExpedienteLN : ICrearExpedienteLN
    {
        private readonly ICrearExpedienteAD _crearAD;

        public CrearExpedienteLN()
        {
            _crearAD = new CrearExpedienteAD();
        }

        public string Crear(ExpedienteDto dto)
        {
            if (_crearAD.ExisteExpedientePorCedula(dto.Identificacion))
                return "El paciente ya posee un expediente registrado en el sistema.";

            _crearAD.Crear(dto);
            return null;
        }
    }
}
