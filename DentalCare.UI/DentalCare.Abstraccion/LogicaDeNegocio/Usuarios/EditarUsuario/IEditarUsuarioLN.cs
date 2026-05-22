using DentalCare.Abstraccion.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.EditarUsuario
{
    public interface IEditarUsuarioLN
    {
        string Editar(UsuarioDto dto);
    }
}
