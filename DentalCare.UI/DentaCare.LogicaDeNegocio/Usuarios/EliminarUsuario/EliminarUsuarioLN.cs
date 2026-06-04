using DentalCare.Abstraccion.AccesoADatos.Usuarios.EliminarUsuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.EliminarUsuario;
using DentalCare.AccesoADatos;
using DentalCare.AccesoADatos.Usuarios.EliminarUsuario;
using System.Linq;

namespace DentalCare.LogicaDeNegocio.Usuarios.EliminarUsuario
{
    public class EliminarUsuarioLN : IEliminarUsuarioLN
    {
        private readonly IEliminarUsuarioAD _eliminarAD;
        private readonly Contexto _contexto;

        public EliminarUsuarioLN()
        {
            _eliminarAD = new EliminarUsuarioAD();
            _contexto = new Contexto();
        }

        public string Eliminar(int idUsuario, string aspNetUserIdAdmin)
        {

            var rolAdmin = _contexto.AspNetRoles
                .FirstOrDefault(r => r.Name == "Admin");

            if (rolAdmin == null)
                return "No se encontró el rol Administrador en el sistema.";

            bool esAdmin = _contexto.AspNetUserRoles
                .Any(ur => ur.UserId == aspNetUserIdAdmin && ur.RoleId == rolAdmin.Id);

            if (!esAdmin)
                return "No tiene permisos para eliminar usuarios. Solo el Administrador puede realizar esta acción.";

            _eliminarAD.Eliminar(idUsuario, aspNetUserIdAdmin);
            return null;
        }
    }
}