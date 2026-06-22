using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Citas.CambiarEstadoCita
{
    public interface ICambiarEstadoCitaAD
    {
        void CambiarEstado(int idCita, int idEstado, int idMotivoCancelacion);
        bool ExisteCita(int idCita);
        string ObtenerCorreoPaciente(int idCita);
    }
}
