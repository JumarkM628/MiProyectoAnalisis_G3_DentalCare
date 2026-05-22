using DentalCare.Abstraccion.AccesoADatos.Usuarios.ObtenerTodosLosUsuarios;
using DentalCare.Abstraccion.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Usuarios.ObtenerTodosLosUsuarios
{
    public class ObtenerTodoLosUsuariosAD : IObtenerTodoLosUsuariosAD
    {
        Contexto _elContexto;
        public ObtenerTodoLosUsuariosAD()
        {
            _elContexto = new Contexto();
        }

        public List<UsuarioDto> Obtener()
        {
            var listaUsuarios = (
                from usuario in _elContexto.Usuarios

                join cedula in _elContexto.Cedulas
                    on usuario.IdUsuario equals cedula.IdUsuario into cedulaGrupo
                from cedula in cedulaGrupo.DefaultIfEmpty()

                join telefono in _elContexto.Telefonos
                    on usuario.IdUsuario equals telefono.IdUsuario into telefonoGrupo
                from telefono in telefonoGrupo.DefaultIfEmpty()

                join correo in _elContexto.Correos
                    on usuario.IdUsuario equals correo.IdUsuario into correoGrupo
                from correo in correoGrupo.DefaultIfEmpty()

                join area in _elContexto.Areas
                    on usuario.IdAreaUsuario equals area.IdAreaUsuario

                join especialidad in _elContexto.Especialidades
                    on usuario.IdEspecialidad equals especialidad.IdEspecialidad

                join estado in _elContexto.Estados
                    on usuario.IdEstado equals estado.IdEstado

                select new UsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    AspNetUserId = usuario.ASPNET_USER_ID,
                    Nombre = usuario.Nombre,
                    PrimerApellido = usuario.PrimerApellido,
                    SegundoApellido = usuario.SegundoApellido,
                    TipoCedula = cedula != null ? cedula.TipoCedula : "",
                    NumeroCedula = cedula != null ? cedula.NumeroCedula : "",
                    Telefono = telefono != null ? telefono.Telefono : "",
                    Correo = correo != null ? correo.Correo : "",
                    IdAreaUsuario = usuario.IdAreaUsuario,
                    NombreArea = area.NombreTipoUsuario,
                    IdEspecialidad = usuario.IdEspecialidad,
                    NombreEspecialidad = especialidad.NombreEspecialidad,
                    IdEstado = usuario.IdEstado,
                    NombreEstado = estado.NombreEstado,
                    FechaContratacion = usuario.FechaDeContratacion,
                    FechaCreacion = usuario.FechaDeCreacion
                }
            ).ToList();

            return listaUsuarios;
        }
    }
}
