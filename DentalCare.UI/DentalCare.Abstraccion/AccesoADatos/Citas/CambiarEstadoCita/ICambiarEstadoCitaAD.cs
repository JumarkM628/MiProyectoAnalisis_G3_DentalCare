using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Citas.CambiarEstadoCita
{
    public interface ICambiarEstadoCitaAD
    {

        void CambiarEstado(int idCita, string nombreEstado, int idMotivoCancelacion = 1);
        void CancelarConMotivo(int idCita, string motivoTexto); 
        void RegistrarAsistencia(int idCita, TimeSpan horaInicio);
        void RegistrarAusencia(int idCita);
        void RegistrarFinalizacion(int idCita, TimeSpan horaFin);
        bool ExisteCita(int idCita);
        string ObtenerCorreoPaciente(int idCita);

    }
}
