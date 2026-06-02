using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Usuarios.RecuperarContrasena;

namespace DentalCare.AccesoADatos.Usuarios.RecuperarContrasena
{
    public class RecuperarContrasenaAD : IRecuperarContrasenaAD
    {
        private readonly Contexto _elContexto;

        public RecuperarContrasenaAD()
        {
            _elContexto = new Contexto();
        }

        public bool ExisteCorreo(string correo)
        {
            return _elContexto.Correos.Any(c => c.Correo == correo);
        }
    }
}
