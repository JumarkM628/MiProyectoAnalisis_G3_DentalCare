using DentalCare.Abstraccion.AccesoADatos.Usuarios.EliminarUsuario;
using System;
using System.Linq;

namespace DentalCare.AccesoADatos.Usuarios.EliminarUsuario
{
    public class EliminarUsuarioAD : IEliminarUsuarioAD
    {
        private readonly Contexto _contexto;

        public EliminarUsuarioAD()
        {
            _contexto = new Contexto();
        }

        public void Eliminar(int idUsuario, string aspNetUserIdAdmin)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {

                    var usuario = _contexto.Usuarios
                        .First(u => u.IdUsuario == idUsuario);

                    usuario.IdEstado = 2;
                    usuario.FechaDeCreacion = DateTime.Now;
                    _contexto.SaveChanges();

                    var correos = _contexto.Correos
                        .Where(c => c.IdUsuario == idUsuario).ToList();
                    _contexto.Correos.RemoveRange(correos);
                    _contexto.SaveChanges();

                    var telefonos = _contexto.Telefonos
                        .Where(t => t.IdUsuario == idUsuario).ToList();
                    _contexto.Telefonos.RemoveRange(telefonos);
                    _contexto.SaveChanges();

                    var cedulas = _contexto.Cedulas
                        .Where(c => c.IdUsuario == idUsuario).ToList();
                    _contexto.Cedulas.RemoveRange(cedulas);
                    _contexto.SaveChanges();

                    // Eliminar el usuario principal
                    _contexto.Usuarios.Remove(usuario);
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
    }
}
