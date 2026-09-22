using DentalCare.Abstraccion.Modelo.Usuarios;

namespace DentalCare.Abstraccion.AccesoADatos.Usuarios.RegistrarUsuarios
{
    public interface IRegistrarUsuariosAD
    {
        void RegistrarUsuario(UsuarioDto dto);
        bool ExisteCedula(string numeroCedula);
        // ExisteCorreo eliminado — Identity maneja unicidad de email en AspNetUsers
    }
}