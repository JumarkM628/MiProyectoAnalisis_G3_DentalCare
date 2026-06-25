using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Citas.CambiarEstadoCita
{
    public interface ICambiarEstadoCitaLN
    {
        string Cancelar(int idCita, string motivoCancelacion); 
        string Rechazar(int idCita);
        string Confirmar(int idCita);
        string Asistir(int idCita, TimeSpan horaInicio);
        string Finalizar(int idCita, TimeSpan horaFin);
        string EditarEstado(int idCita, string nuevoEstado, int motivoCancelacion = 1);
        string Ausente(int idCita);
    }
}