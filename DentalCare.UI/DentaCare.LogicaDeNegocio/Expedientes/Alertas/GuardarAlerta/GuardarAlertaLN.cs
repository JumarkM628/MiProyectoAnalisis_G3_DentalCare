using DentalCare.Abstraccion.AccesoADatos.Expediente.Alerta.GuardarAlerta;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.Alertas.GuardarAlerta;
using DentalCare.Abstraccion.Modelo.Alertas;
using DentalCare.AccesoADatos.Alertas.GuardarAlerta;

namespace DentalCare.LogicaDeNegocio.Expedientes.Alertas.GuardarAlerta
{
    public class GuardarAlertaLN : IGuardarAlertaLN
    {
        private readonly IGuardarAlertaAD _guardarAD;

        public GuardarAlertaLN()
        {
            _guardarAD = new GuardarAlertaAD();
        }

        public string Guardar(int idExpediente, AlertaDto dto)
        {
            _guardarAD.Guardar(idExpediente, dto);
            return null;
        }
    }
}
