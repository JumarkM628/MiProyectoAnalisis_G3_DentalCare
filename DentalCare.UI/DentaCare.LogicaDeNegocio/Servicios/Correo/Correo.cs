using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace DentaCare.LogicaDeNegocio.Servicios.Correo
{
    internal class Correo
    {
        public void EnviarRecuperacion(string correoDestino)
        {
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Tls12;

            string correoOrigen = "dentalcaremailtester@gmail.com";
            string appPassword = "hwubwchnhlnubilz";

            System.Diagnostics.Debug.WriteLine($"Correo:[{correoOrigen}]");
            System.Diagnostics.Debug.WriteLine($"Password:[{appPassword}]");
            System.Diagnostics.Debug.WriteLine($"Password Length:[{appPassword.Length}]");

            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress(correoOrigen, "Clínica Dental Dra. Rebeca");
            mensaje.To.Add(correoDestino);
            mensaje.Subject = "Recuperación de contraseña";
            mensaje.Body =
                "<p>Se solicitó una recuperación de contraseña.</p>" +
                "<p>Ingrese al siguiente enlace para restablecerla:</p>" +
                "<p><a href='https://localhost:44340/Account/ResetPassword'>Restablecer contraseña</a></p>" +
                "<p><small>Si no solicitó esto, ignore este correo.</small></p>";
            mensaje.IsBodyHtml = true;

            SmtpClient cliente = new SmtpClient();
            cliente.Host = "smtp.gmail.com";
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.UseDefaultCredentials = false;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.Credentials = new NetworkCredential(correoOrigen, appPassword);

            try
            {
                cliente.Send(mensaje);
            }
            catch (SmtpException ex)
            {
                System.Diagnostics.Debug.WriteLine("SMTP MESSAGE:");
                System.Diagnostics.Debug.WriteLine(ex.Message);

                System.Diagnostics.Debug.WriteLine("SMTP STATUS:");
                System.Diagnostics.Debug.WriteLine(ex.StatusCode);

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine("INNER:");
                    System.Diagnostics.Debug.WriteLine(ex.InnerException.ToString());
                }

                throw;
            }
        }
    }
}
