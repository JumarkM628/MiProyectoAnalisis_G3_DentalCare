using DentalCare.Abstraccion.AccesoADatos.Usuarios.ObtenerTodosLosUsuarios;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Usuarios.ObtenerTodosLosUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Usuarios.ObtenerTodosLosUsuarios
{
    public class ObtenerTodosLosUsuariosLN: IObtenerTodosLosUsuariosLN
    {
        IObtenerTodoLosUsuariosAD _obtenerTodosLosUsuariosAD;

        public ObtenerTodosLosUsuariosLN()
        {
            _obtenerTodosLosUsuariosAD = new ObtenerTodoLosUsuariosAD();
        }

        public List<UsuarioDto> Obtener()
        {
            List<UsuarioDto> listaUsuarios = _obtenerTodosLosUsuariosAD.Obtener();
            listaUsuarios = listaUsuarios.OrderBy(elUsuario => elUsuario.PrimerApellido).ThenBy(elUsuario => elUsuario.SegundoApellido).ThenBy(elUsuario => elUsuario.Nombre).ToList();
            return listaUsuarios;
        }
    }
}
