using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Usuarios.RecuperarContrasena
{
    public interface IRecuperarContrasenaAD
    {
        bool ExisteCorreo(string correo);
    }
}
