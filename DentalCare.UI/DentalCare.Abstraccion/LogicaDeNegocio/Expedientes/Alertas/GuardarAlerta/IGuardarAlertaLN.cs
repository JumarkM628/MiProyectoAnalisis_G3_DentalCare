using DentalCare.Abstraccion.Modelo.Alertas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.Alertas.GuardarAlerta
{
    public interface IGuardarAlertaLN
    {
        string Guardar(int idExpediente, AlertaDto dto);
    }
}
