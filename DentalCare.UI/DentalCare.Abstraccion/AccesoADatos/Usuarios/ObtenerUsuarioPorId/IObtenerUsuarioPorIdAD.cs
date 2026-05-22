using DentalCare.Abstraccion.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Usuarios.ObtenerUsuarioPorId
{
    public interface IObtenerUsuarioPorIdAD
    {
        UsuarioDto Obtener(int idUsuario);
    }
}
