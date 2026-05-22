using DentalCare.Abstraccion.AccesoADatos.Usuarios.RegistrarUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.RegistrarUsuario;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Usuarios.RegistrarUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Usuarios.RegistrarUsuarios
{
    public class RegistrarUsuariosLN: IRegistrarUsuariosLN
    {
        IRegistrarUsuariosAD _registrarUsuariosAD;

        public RegistrarUsuariosLN()
        {
            _registrarUsuariosAD = new RegistrarUsuariosAD();
        }

        public string RegistrarUsuario(UsuarioDto dto)
        {
            if (_registrarUsuariosAD.ExisteCedula(dto.NumeroCedula))
                return "Ya existe un usuario registrado con esa cédula.";

            if (_registrarUsuariosAD.ExisteCorreo(dto.Correo))
                return "Ya existe un usuario registrado con ese correo electrónico.";

            _registrarUsuariosAD.RegistrarUsuario(dto);
            return null;
        }
    }
}
