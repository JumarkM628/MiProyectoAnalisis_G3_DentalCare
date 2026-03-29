using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Citas
{
    public class CitaTDO
    {
        public int Id_cita { get; set; }
        [Display(Name = "Descripcion de la Cita")]
        [Required]
        [StringLength(10)]
        public string descripcion { get; set; }
        [Display(Name = "Fecha de la cita")]
        public DateTime Fecha_Cita { get; set; }
        [Display(Name = "Fecha de registro")]
        public DateTime Fecha_Registro { get; set; }
        [Display(Name = "Fecha de modificacion")]
        public DateTime? Fecha_Modificacion { get; set; }
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        [Display(Name = "Id Usuario")]
        [Required()]
        public int Id_Usuario { get; set; }
        [Display(Name = "Id de doctora")]
        [Required()]
        public int Id_Doctor { get; set; }

    }
}
