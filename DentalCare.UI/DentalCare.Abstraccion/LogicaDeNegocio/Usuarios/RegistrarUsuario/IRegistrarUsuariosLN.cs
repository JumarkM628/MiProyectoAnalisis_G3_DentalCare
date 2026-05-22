using DentalCare.Abstraccion.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.RegistrarUsuario
{
    public interface IRegistrarUsuariosLN
    {
        string RegistrarUsuario(UsuarioDto dto);
    }
}
