using DentalCare.Abstraccion.AccesoADatos.Usuarios.EditarUsuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.EditarUsuario;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Usuarios.EditarUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Usuarios.EditarUsuario
{
    public class EditarUsuarioLN: IEditarUsuarioLN
    {
        private readonly IEditarUsuarioAD _editarUsuarioAD;

        public EditarUsuarioLN()
        {
            _editarUsuarioAD = new EditarUsuarioAD();
        }

        public string Editar(UsuarioDto dto)
        {

            if (_editarUsuarioAD.ExisteCedulaEnOtroUsuario(dto.IdUsuario, dto.NumeroCedula))
                return "Ya existe otro usuario registrado con esa cédula.";

            if (_editarUsuarioAD.ExisteCorreoEnOtroUsuario(dto.IdUsuario, dto.Correo))
                return "Ya existe otro usuario registrado con ese correo electrónico.";

            dto.FechaCreacion = DateTime.Now;

            _editarUsuarioAD.Editar(dto);
            return null;
        }
    }
}
