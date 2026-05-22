using DentalCare.Abstraccion.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Usuarios.RegistrarUsuarios
{
    public interface IRegistrarUsuariosAD
    {
        void RegistrarUsuario(UsuarioDto dto);
        bool ExisteCedula(string numeroCedula);
        bool ExisteCorreo(string correo);
    }
}
