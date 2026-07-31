using DentalCare.Abstraccion.AccesoADatos.Usuarios.EditarUsuario;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Entidades.Usuarios;
using System;
using System.Linq;

namespace DentalCare.AccesoADatos.Usuarios.EditarUsuario
{
    public class EditarUsuarioAD : IEditarUsuarioAD
    {
        private readonly Contexto _contexto;

        public EditarUsuarioAD()
        {
            _contexto = new Contexto();
        }

        public void Editar(UsuarioDto dto)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    // 1. Actualizar FIDE_USUARIO_TB
                    var usuario = _contexto.Usuarios
                        .First(u => u.IdUsuario == dto.IdUsuario);

                    usuario.Nombre = dto.Nombre;
                    usuario.PrimerApellido = dto.PrimerApellido;
                    usuario.SegundoApellido = dto.SegundoApellido ?? string.Empty;
                    usuario.IdAreaUsuario = dto.IdAreaUsuario;
                    usuario.IdEspecialidad = dto.IdEspecialidad;
                    usuario.IdEstado = dto.IdEstado;
                    usuario.FechaDeContratacion = dto.FechaContratacion;
                    usuario.FechaDeCreacion = dto.FechaCreacion;
                    _contexto.SaveChanges();

                    // 2. Actualizar FIDE_CEDULA_TB
                    var cedula = _contexto.Cedulas
                        .FirstOrDefault(c => c.IdUsuario == dto.IdUsuario);

                    if (cedula != null)
                    {
                        cedula.TipoCedula = dto.TipoCedula;
                        cedula.NumeroCedula = dto.NumeroCedula;
                    }
                    else
                    {
                        _contexto.Cedulas.Add(new CedulaEntidad
                        {
                            IdUsuario = dto.IdUsuario,
                            TipoCedula = dto.TipoCedula,
                            NumeroCedula = dto.NumeroCedula,
                            IdEstado = dto.IdEstado
                        });
                    }
                    _contexto.SaveChanges();

                    // 3. Actualizar FIDE_TELEFONO_TB
                    var telefono = _contexto.Telefonos
                        .FirstOrDefault(t => t.IdUsuario == dto.IdUsuario);

                    if (telefono != null)
                    {
                        telefono.Telefono = dto.Telefono;
                    }
                    else
                    {
                        _contexto.Telefonos.Add(new TelefonoEntidad
                        {
                            IdUsuario = dto.IdUsuario,
                            Telefono = dto.Telefono,
                            IdEstado = dto.IdEstado
                        });
                    }
                    _contexto.SaveChanges();

                    // El correo ya no se actualiza en FIDE_CORREO_TB
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

        // Solo validación de cédula — el correo lo maneja Identity
        public bool ExisteCedulaEnOtroUsuario(int idUsuario, string numeroCedula)
        {
            return _contexto.Cedulas
                .Any(c => c.NumeroCedula == numeroCedula && c.IdUsuario != idUsuario);
        }
    }
}