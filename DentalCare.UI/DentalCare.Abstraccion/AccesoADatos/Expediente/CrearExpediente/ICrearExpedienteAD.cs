using DentalCare.Abstraccion.Modelo.Expedientes;


namespace DentalCare.Abstraccion.AccesoADatos.Expediente.CrearExpediente
{
    public interface ICrearExpedienteAD
    {
        void Crear(ExpedienteDto dto);
        bool ExisteExpedientePorCedula(string numeroCedula);
    }
}
