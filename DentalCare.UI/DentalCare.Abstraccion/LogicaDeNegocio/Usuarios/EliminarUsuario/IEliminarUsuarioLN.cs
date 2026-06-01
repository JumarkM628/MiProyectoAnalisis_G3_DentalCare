using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Usuarios.EliminarUsuario
{
    public interface IEliminarUsuarioLN
    {
        string Eliminar(int idUsuario, string aspNetUserIdAdmin);
    }
}
