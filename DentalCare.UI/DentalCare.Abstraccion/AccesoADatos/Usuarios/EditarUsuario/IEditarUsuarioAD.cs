using DentalCare.Abstraccion.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Usuarios.EditarUsuario
{
    public interface IEditarUsuarioAD
    {
        void Editar(UsuarioDto dto);
        bool ExisteCedulaEnOtroUsuario(int idUsuario, string numeroCedula);
        bool ExisteCorreoEnOtroUsuario(int idUsuario, string correo);
    }
}
