using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Usuarios
{
    [Table("AspNetRoles")]
    public class AspNetRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
