using System;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Usuarios.DesactivarUsuario;

namespace DentalCare.AccesoADatos.Usuarios.DesactivarUsuario
{
    public class DesactivarUsuarioAD : IDesactivarUsuarioAD
    {
        private readonly Contexto _contexto;

        public DesactivarUsuarioAD()
        {
            _contexto = new Contexto();
        }

        public void Desactivar(int idUsuario)
        {
            var usuario = _contexto.Usuarios
                .FirstOrDefault(u => u.IdUsuario == idUsuario);

            if (usuario == null) return;

            var estadoInactivo = _contexto.Estados
                .FirstOrDefault(e => e.NombreEstado.ToLower() == "inactivo");

            if (estadoInactivo == null) return;

            usuario.IdEstado = estadoInactivo.IdEstado;

            var aspNetUser = _contexto.AspNetUsers
                .FirstOrDefault(u => u.Id == usuario.ASPNET_USER_ID);

            if (aspNetUser != null)
            {
                aspNetUser.LockoutEnabled = true;
                aspNetUser.LockoutEndDateUtc = DateTime.UtcNow.AddYears(100);
            }

            _contexto.SaveChanges();
        }
    }
}