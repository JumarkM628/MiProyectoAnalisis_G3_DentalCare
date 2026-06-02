using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentaCare.LogicaDeNegocio.Servicios.Correo;
using DentalCare.Abstraccion.AccesoADatos.Usuarios.RecuperarContrasena;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.RecuperarContrasena;
using DentalCare.AccesoADatos.Usuarios.RecuperarContrasena;

namespace DentaCare.LogicaDeNegocio.Usuarios.RecuperarContrasena
{
    public class RecuperarContrasenaLN : IRecuperarContrasenaLN
    {
        private readonly IRecuperarContrasenaAD _recuperarContrasenaAD;

        public RecuperarContrasenaLN(RecuperarContrasenaAD recuperarContrasenaAD)
        {
            _recuperarContrasenaAD = new RecuperarContrasenaAD();
            _recuperarContrasenaAD = recuperarContrasenaAD;
        }

        public string RecuperarContrasena(string correo)
        {
            if (!_recuperarContrasenaAD.ExisteCorreo(correo))
            {
                return "El correo ingresado no se encuentra registrado.";
            }
            Correo correoServicio = new Correo();

            correoServicio.EnviarRecuperacion(correo);

            return null;
        }
    }
}
