using DentalCare.Abstraccion.AccesoADatos.Usuarios.EditarUsuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.EditarUsuario;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Usuarios.EditarUsuario;
using System;

namespace DentaCare.LogicaDeNegocio.Usuarios.EditarUsuario
{
    public class EditarUsuarioLN : IEditarUsuarioLN
    {
        private readonly IEditarUsuarioAD _editarUsuarioAD;

        public EditarUsuarioLN()
        {
            _editarUsuarioAD = new EditarUsuarioAD();
        }

        public string Editar(UsuarioDto dto)
        {
            // Solo validamos cédula duplicada en otro usuario
            // El correo lo maneja Identity en AspNetUsers
            if (_editarUsuarioAD.ExisteCedulaEnOtroUsuario(dto.IdUsuario, dto.NumeroCedula))
                return "Ya existe otro usuario registrado con esa cédula.";

            dto.FechaCreacion = DateTime.Now;

            _editarUsuarioAD.Editar(dto);
            return null;
        }
    }
}