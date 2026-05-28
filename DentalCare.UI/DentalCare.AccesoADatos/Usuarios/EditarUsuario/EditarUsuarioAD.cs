using DentalCare.Abstraccion.AccesoADatos.Usuarios.EditarUsuario;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Entidades.Usuarios;   // ← importante para AspNetUserRolEntidad
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
                    var usuario = _contexto.Usuarios.First(u => u.IdUsuario == dto.IdUsuario);
                    usuario.Nombre = dto.Nombre;
                    usuario.PrimerApellido = dto.PrimerApellido;
                    usuario.SegundoApellido = dto.SegundoApellido ?? string.Empty;
                    usuario.IdAreaUsuario = dto.IdAreaUsuario;
                    usuario.IdEspecialidad = dto.IdEspecialidad;
                    usuario.IdEstado = dto.IdEstado;
                    usuario.FechaDeContratacion = dto.FechaContratacion;
                    usuario.FechaDeCreacion = dto.FechaCreacion;

                    var cedula = _contexto.Cedulas.FirstOrDefault(c => c.IdUsuario == dto.IdUsuario);
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

                    var telefono = _contexto.Telefonos.FirstOrDefault(t => t.IdUsuario == dto.IdUsuario);
                    if (telefono != null)
                        telefono.Telefono = dto.Telefono;
                    else
                    {
                        _contexto.Telefonos.Add(new TelefonoEntidad
                        {
                            IdUsuario = dto.IdUsuario,
                            Telefono = dto.Telefono,
                            IdEstado = dto.IdEstado
                        });
                    }

                    var correo = _contexto.Correos.FirstOrDefault(c => c.IdUsuario == dto.IdUsuario);
                    if (correo != null)
                        correo.Correo = dto.Correo;
                    else
                    {
                        _contexto.Correos.Add(new CorreoEntidad
                        {
                            IdUsuario = dto.IdUsuario,
                            Correo = dto.Correo,
                            IdEstado = dto.IdEstado
                        });
                    }

                    var rolActual = _contexto.AspNetUserRoles
                        .FirstOrDefault(ur => ur.UserId == dto.AspNetUserId);

                    if (rolActual != null)
                    {
                        if (rolActual.RoleId != dto.RoleId)
                        {
                            _contexto.AspNetUserRoles.Remove(rolActual);
                            _contexto.AspNetUserRoles.Add(new AspNetUserRolEntidad 
                            {
                                UserId = dto.AspNetUserId,
                                RoleId = dto.RoleId
                            });
                        }
                    }
                    else
                    {
                        _contexto.AspNetUserRoles.Add(new AspNetUserRolEntidad
                        {
                            UserId = dto.AspNetUserId,
                            RoleId = dto.RoleId
                        });
                    }

                    _contexto.SaveChanges();
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public bool ExisteCedulaEnOtroUsuario(int idUsuario, string numeroCedula)
        {
            return _contexto.Cedulas
                .Any(c => c.NumeroCedula == numeroCedula && c.IdUsuario != idUsuario);
        }

        public bool ExisteCorreoEnOtroUsuario(int idUsuario, string correo)
        {
            return _contexto.Correos
                .Any(c => c.Correo == correo && c.IdUsuario != idUsuario);
        }
    }
}