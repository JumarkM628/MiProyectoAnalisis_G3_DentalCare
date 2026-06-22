using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Citas.CambiarEstadoCita
{
    public interface ICambiarEstadoCitaLN
    {
        string Cancelar(int idCita);
        string Rechazar(int idCita);
        string Confirmar(int idCita);
    }
}
