using DentalCare.Abstraccion.AccesoADatos.Usuarios.RegistrarUsuarios;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Entidades;
using DentalCare.AccesoADatos.Entidades.Usuarios;
using System;
using System.Linq;

namespace DentalCare.AccesoADatos.Usuarios.RegistrarUsuario
{
    public class RegistrarUsuariosAD : IRegistrarUsuariosAD
    {
        private readonly Contexto _elContexto;

        public RegistrarUsuariosAD()
        {
            _elContexto = new Contexto();
        }

        public void RegistrarUsuario(UsuarioDto dto)
        {
            using (var transaccion = _elContexto.Database.BeginTransaction())
            {
                try
                {
                    int nuevoId = _elContexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_USUARIO), 0) + 1 FROM FIDE_USUARIO_TB")
                        .FirstOrDefault();

                    _elContexto.Usuarios.Add(new UsuariosEntidad
                    {
                        IdUsuario = nuevoId,
                        ASPNET_USER_ID = dto.AspNetUserId,
                        Nombre = dto.Nombre,
                        PrimerApellido = dto.PrimerApellido,
                        SegundoApellido = dto.SegundoApellido ?? string.Empty,
                        IdAreaUsuario = dto.IdAreaUsuario,
                        IdEspecialidad = dto.IdEspecialidad,
                        IdEstado = dto.IdEstado,
                        FechaDeContratacion = dto.FechaContratacion,
                        FechaDeCreacion = DateTime.Now
                    });
                    _elContexto.SaveChanges();

                    _elContexto.Cedulas.Add(new CedulaEntidad
                    {
                        IdUsuario = nuevoId,
                        TipoCedula = dto.TipoCedula,
                        NumeroCedula = dto.NumeroCedula,
                        IdEstado = dto.IdEstado
                    });
                    _elContexto.SaveChanges();

                    _elContexto.Telefonos.Add(new TelefonoEntidad
                    {
                        IdUsuario = nuevoId,
                        Telefono = dto.Telefono,
                        IdEstado = dto.IdEstado
                    });
                    _elContexto.SaveChanges();

                    // El correo ya no se inserta en FIDE_CORREO_TB
                    // porque se obtiene directamente de AspNetUsers.Email

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public bool ExisteCedula(string numeroCedula)
            => _elContexto.Cedulas.Any(c => c.NumeroCedula == numeroCedula);
    }
}