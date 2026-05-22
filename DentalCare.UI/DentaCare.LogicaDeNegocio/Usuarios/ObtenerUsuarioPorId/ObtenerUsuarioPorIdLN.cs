using DentalCare.Abstraccion.AccesoADatos.Usuarios.ObtenerUsuarioPorId;
using DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.ObtenerUsuarioPorId;
using DentalCare.Abstraccion.Modelo.Usuarios;
using DentalCare.AccesoADatos.Usuarios.ObtenerUsuarioPorId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Usuarios.ObtenerUsuarioPorId
{
    public class ObtenerUsuarioPorIdLN: IObtenerUsuarioPorIdLN
    {
        private readonly IObtenerUsuarioPorIdAD _obtenerPorIdAD;

        public ObtenerUsuarioPorIdLN()
        {
            _obtenerPorIdAD = new ObtenerUsuarioPorIdAD();
        }

        public UsuarioDto Obtener(int idUsuario)
        {
            return _obtenerPorIdAD.Obtener(idUsuario);
        }
    }
}
