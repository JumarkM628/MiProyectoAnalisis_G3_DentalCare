using DentalCare.Abstraccion.AccesoADatos.Usuarios.DesactivarUsuario;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.DesactivarUsuario;
using DentalCare.AccesoADatos.Usuarios.DesactivarUsuario;

namespace DentalCare.LogicaDeNegocio.Usuarios.DesactivarUsuario
{
    public class DesactivarUsuarioLN : IDesactivarUsuarioLN
    {
        private readonly IDesactivarUsuarioAD _desactivarAD;

        public DesactivarUsuarioLN()
        {
            _desactivarAD = new DesactivarUsuarioAD();
        }

        public string Desactivar(int idUsuario)
        {
            _desactivarAD.Desactivar(idUsuario);
            return null;
        }
    }
}