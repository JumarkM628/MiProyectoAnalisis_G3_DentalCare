using DentalCare.Abstraccion.Modelo.Alertas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Expediente.Alerta.GuardarAlerta
{
    public interface IGuardarAlertaAD
    {
        void Guardar(int idExpediente, AlertaDto dto);
    }
}
