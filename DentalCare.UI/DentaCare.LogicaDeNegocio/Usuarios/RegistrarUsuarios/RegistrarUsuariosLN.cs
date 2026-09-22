using DentalCare.Abstraccion.AccesoADatos.Usuarios.RegistrarUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.RegistrarUsuario;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Usuarios.RegistrarUsuario;

namespace DentaCare.LogicaDeNegocio.Usuarios.RegistrarUsuarios
{
    public class RegistrarUsuariosLN : IRegistrarUsuariosLN
    {
        IRegistrarUsuariosAD _registrarUsuariosAD;

        public RegistrarUsuariosLN()
        {
            _registrarUsuariosAD = new RegistrarUsuariosAD();
        }

        public string RegistrarUsuario(UsuarioDto dto)
        {
            // Solo validamos cédula duplicada
            // El correo lo maneja Identity al momento del registro de cuenta
            if (_registrarUsuariosAD.ExisteCedula(dto.NumeroCedula))
                return "Ya existe un usuario registrado con esa cédula.";

            _registrarUsuariosAD.RegistrarUsuario(dto);
            return null;
        }
    }
}