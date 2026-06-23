using DentalCare.Abstraccion.AccesoADatos.Citas.CambiarEstadoCita;
using DentalCare.Abstraccion.LogicaDeNegocio.Citas.CambiarEstadoCita;
using DentalCare.AccesoADatos.Citas.CambiarEstadoCita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Citas.CambiarEstadoCita
{
    public class CambiarEstadoCitaLN : ICambiarEstadoCitaLN
    {
        private readonly ICambiarEstadoCitaAD _cambiarAD;

        public CambiarEstadoCitaLN()
        {
            _cambiarAD = new CambiarEstadoCitaAD();
        }

        // Escenario 1 y 3: cancelar — libera el horario cambiando estado
        public string Cancelar(int idCita)
        {
            if (!_cambiarAD.ExisteCita(idCita))
                return "No se encontró la cita.";
            // Usar el nombre "Cancelada"
            _cambiarAD.CambiarEstado(idCita, CambiarEstadoCitaAD.ESTADO_CANCELADA, 1);
            return null;
        }

        // Escenario 4: rechazar y notificar al paciente por correo
        public string Rechazar(int idCita)
        {
            if (!_cambiarAD.ExisteCita(idCita))
                return "No se encontró la cita.";
            _cambiarAD.CambiarEstado(idCita, CambiarEstadoCitaAD.ESTADO_RECHAZADA, 1);
            // ... notificación por correo ...
            return null;
        }

        public string Confirmar(int idCita)
        {
            if (!_cambiarAD.ExisteCita(idCita))
                return "No se encontró la cita.";
            _cambiarAD.CambiarEstado(idCita, CambiarEstadoCitaAD.ESTADO_CONFIRMADA, 1);
            return null;
        }
        public string Asistir(int idCita, TimeSpan horaInicio)
        {
            if (!_cambiarAD.ExisteCita(idCita))
                return "No se encontró la cita.";
            _cambiarAD.RegistrarAsistencia(idCita, horaInicio);
            return null;
        }

        public string Finalizar(int idCita, TimeSpan horaFin)
        {
            if (!_cambiarAD.ExisteCita(idCita))
                return "No se encontró la cita.";
            _cambiarAD.RegistrarFinalizacion(idCita, horaFin);
            return null;
        }

        public string EditarEstado(int idCita, string nuevoEstado, int motivoCancelacion = 1)
        {
            if (!_cambiarAD.ExisteCita(idCita))
                return "No se encontró la cita.";
            _cambiarAD.CambiarEstado(idCita, nuevoEstado, motivoCancelacion);
            return null;
        }

        public string Ausente(int idCita)
        {
            if (!_cambiarAD.ExisteCita(idCita))
                return "No se encontró la cita.";
            _cambiarAD.RegistrarAusencia(idCita);
            return null;
        }

        // ---------------------------------------------------------------
        // Envío de correo de notificación al paciente (Escenario 4)
        // ---------------------------------------------------------------
        private void EnviarCorreoRechazo(string destinatario, int idCita)
        {
            try
            {
                using (var cliente = new SmtpClient("smtp.gmail.com", 587))
                {
                    cliente.EnableSsl = true;
                    cliente.UseDefaultCredentials = false;
                    cliente.Credentials = new NetworkCredential(
                        "dentalcaremailtester@gmail.com",
                        "hwubwchnhlnubilz"); // igual que en IdentityConfig

                    var correo = new MailMessage
                    {
                        From = new MailAddress("mirandacjumark23@gmail.com", "DentalCare"),
                        Subject = "Solicitud de cita — DentalCare",
                        IsBodyHtml = true,
                        Body = $@"
                            <div style='font-family:Arial,sans-serif; max-width:600px; margin:auto;'>
                                <h2 style='color:#c0392b;'>Solicitud de Cita Rechazada</h2>
                                <p>Lamentamos informarte que tu solicitud de cita (#{idCita}) 
                                   no pudo ser aprobada en este momento.</p>
                                <p>Por favor contactanos o solicitá una nueva cita en otro horario.</p>
                                <hr/>
                                <p style='color:#aaa; font-size:11px;'>DentalCare — Sistema de Gestión Clínica</p>
                            </div>"
                    };
                    correo.To.Add(destinatario);
                    cliente.Send(correo);
                }
            }
            catch
            {
                // Si falla el correo no interrumpimos el flujo principal
            }
        }
    }
}
