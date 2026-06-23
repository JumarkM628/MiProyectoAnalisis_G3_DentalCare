using DentalCare.AccesoADatos.Citas.ObtenerCitaPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Citas.RecordatorioCitaServicios
{
    public class RecordatorioCitaServicio
    {
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SmtpUsuario = "mirandacjumark23@gmail.com";
        private const string SmtpPassword = "TU_CONTRASEÑA_DE_APLICACION";
        private const string CorreoAdmin = "mirandacjumark23@gmail.com";
        private const string NombreRemitente = "DentalCare";

        /// <summary>
        /// Método que Hangfire ejecuta cada hora.
        /// Busca citas en las próximas 24 horas y envía recordatorios.
        /// </summary>
        public void EnviarRecordatorios()
        {
            var ad = new ObtenerCitasPacienteAD();
            var citasProximas = ad.ObtenerCitasProximas24Horas();

            foreach (var cita in citasProximas)
            {
                if (string.IsNullOrEmpty(cita.CorreoPaciente)) continue;

                try
                {
                    // Escenario 1: enviar recordatorio
                    EnviarCorreo(
                        destinatario: cita.CorreoPaciente,
                        asunto: "Recordatorio de cita — DentalCare",
                        cuerpo: $@"
                            <div style='font-family:Arial,sans-serif; max-width:600px; margin:auto;'>
                                <h2 style='color:#2c3e50;'>Recordatorio de Cita</h2>
                                <p>Hola <strong>{cita.NombrePaciente}</strong>,</p>
                                <p>Te recordamos que tenés una cita programada:</p>
                                <table style='width:100%; border-collapse:collapse; margin:16px 0;'>
                                    <tr style='background:#f5f7fa;'>
                                        <td style='padding:10px; font-weight:bold;'>Fecha</td>
                                        <td style='padding:10px;'>{(cita.Fecha.HasValue ? cita.Fecha.Value.ToString("dd/MM/yyyy") : "—")}</td>
                                    </tr>
                                    <tr>
                                        <td style='padding:10px; font-weight:bold;'>Hora</td>
                                        <td style='padding:10px;'>{cita.HoraString}</td>
                                    </tr>
                                    <tr style='background:#f5f7fa;'>
                                        <td style='padding:10px; font-weight:bold;'>Motivo</td>
                                        <td style='padding:10px;'>{cita.NombreMotivo}</td>
                                    </tr>
                                </table>
                                <p>Si no podés asistir, por favor cancelá tu cita con anticipación.</p>
                                <hr/>
                                <p style='color:#aaa; font-size:11px;'>DentalCare — Sistema de Gestión Clínica</p>
                            </div>");

                    // Escenario 2: registrar envío exitoso en log
                    System.Diagnostics.Trace.TraceInformation(
                        $"[DentalCare] Recordatorio enviado — Cita #{cita.IdCita} " +
                        $"a {cita.CorreoPaciente} — {DateTime.Now:dd/MM/yyyy HH:mm}");
                }
                catch (Exception ex)
                {
                    // Escenario 3: registrar error y notificar al admin
                    System.Diagnostics.Trace.TraceError(
                        $"[DentalCare] ERROR enviando recordatorio — Cita #{cita.IdCita} " +
                        $"— {ex.Message} — {DateTime.Now:dd/MM/yyyy HH:mm}");

                    try
                    {
                        EnviarCorreo(
                            destinatario: CorreoAdmin,
                            asunto: $"ERROR — Recordatorio cita #{cita.IdCita} no enviado",
                            cuerpo: $@"
                                <div style='font-family:Arial,sans-serif;'>
                                    <h3 style='color:#c0392b;'>Error en envío de recordatorio</h3>
                                    <p><strong>Cita:</strong> #{cita.IdCita}</p>
                                    <p><strong>Paciente:</strong> {cita.NombrePaciente}</p>
                                    <p><strong>Correo destino:</strong> {cita.CorreoPaciente}</p>
                                    <p><strong>Error:</strong> {ex.Message}</p>
                                    <p><strong>Fecha:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</p>
                                </div>");
                    }
                    catch
                    {
                        // Si falla incluso el correo al admin, solo queda el log
                    }
                }
            }
        }

        private void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            using (var cliente = new SmtpClient(SmtpHost, SmtpPort))
            {
                cliente.EnableSsl = true;
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new NetworkCredential(SmtpUsuario, SmtpPassword);

                var correo = new MailMessage
                {
                    From = new MailAddress(SmtpUsuario, NombreRemitente),
                    Subject = asunto,
                    Body = cuerpo,
                    IsBodyHtml = true
                };
                correo.To.Add(destinatario);
                cliente.Send(correo);
            }
        }
    }
}
