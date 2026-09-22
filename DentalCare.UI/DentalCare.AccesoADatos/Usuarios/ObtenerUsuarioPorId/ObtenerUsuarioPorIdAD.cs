using DentalCare.Abstraccion.AccesoADatos.Usuarios.ObtenerUsuarioPorId;
using DentalCare.Abstraccion.Modelo.Usuarios;
using System.Linq;

namespace DentalCare.AccesoADatos.Usuarios.ObtenerUsuarioPorId
{
    public class ObtenerUsuarioPorIdAD : IObtenerUsuarioPorIdAD
    {
        private readonly Contexto _contexto;

        public ObtenerUsuarioPorIdAD()
        {
            _contexto = new Contexto();
        }

        public UsuarioDto Obtener(int idUsuario)
        {
            var resultado = (
                from usuario in _contexto.Usuarios
                where usuario.IdUsuario == idUsuario

                join cedula in _contexto.Cedulas
                    on usuario.IdUsuario equals cedula.IdUsuario into cedulaGrupo
                from cedula in cedulaGrupo.DefaultIfEmpty()

                join telefono in _contexto.Telefonos
                    on usuario.IdUsuario equals telefono.IdUsuario into telefonoGrupo
                from telefono in telefonoGrupo.DefaultIfEmpty()

                    // JOIN con AspNetUsers para obtener el Email directamente
                join aspNet in _contexto.AspNetUsers
                    on usuario.ASPNET_USER_ID equals aspNet.Id into aspNetGrupo
                from aspNet in aspNetGrupo.DefaultIfEmpty()

                join area in _contexto.Areas
                    on usuario.IdAreaUsuario equals area.IdAreaUsuario

                join especialidad in _contexto.Especialidades
                    on usuario.IdEspecialidad equals especialidad.IdEspecialidad

                join estado in _contexto.Estados
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
                    // Correo viene de AspNetUsers.Email
                    Correo = aspNet != null ? aspNet.Email : "",
                    IdAreaUsuario = usuario.IdAreaUsuario,
                    NombreArea = area.NombreTipoUsuario,
                    IdEspecialidad = usuario.IdEspecialidad,
                    NombreEspecialidad = especialidad.NombreEspecialidad,
                    IdEstado = usuario.IdEstado,
                    NombreEstado = estado.NombreEstado,
                    FechaContratacion = usuario.FechaDeContratacion,
                    FechaCreacion = usuario.FechaDeCreacion
                }
            ).FirstOrDefault();

            return resultado;
        }
    }
}