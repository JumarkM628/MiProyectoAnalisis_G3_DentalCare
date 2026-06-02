using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Servicios.Correo
{
    internal class Correo
    {
        public void EnviarRecuperacion(string correoDestino)
        {
            MailMessage mensaje = new MailMessage();

            mensaje.From = new MailAddress("DentalCareProyecto@gmail.com");
            mensaje.To.Add(correoDestino);

            mensaje.Subject = "Recuperación de contraseña";

            mensaje.Body =
                "Se solicitó una recuperación de contraseña.\n\n" +
                "Ingrese al siguiente enlace:\n" +
                "https://localhost:44300/Account/ResetPassword";

            SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587);

            cliente.Credentials =
                new NetworkCredential(
                    "DentalCareProyecto@gmail.com",
                    "Password12345");

            cliente.EnableSsl = true;

            cliente.Send(mensaje);
        }
    }
}
