using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.RecuperarContrasena
{
    public interface IRecuperarContrasenaLN
    {
        string RecuperarContrasena(string correo);
    }
}
