using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Bitacora
{
    public class BitacoraDto
    {
        public int Id { get; set; }
        public string Modulo { get; set; }
        public string Accion { get; set; }
        public string Descripcion { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaHora { get; set; }
    }
}