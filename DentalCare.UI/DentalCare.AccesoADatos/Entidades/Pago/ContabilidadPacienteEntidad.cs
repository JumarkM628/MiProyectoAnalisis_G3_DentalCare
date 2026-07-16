using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Pago
{
    [Table("FIDE_CONTABILIDAD_PACIENTE_TB")]
    public class ContabilidadPacienteEntidad
    {
        [Key, Column("ID_USUARIO", Order = 0)]
        public int IdUsuario { get; set; }

        [Key, Column("ID_CONTABILIDAD", Order = 1)]
        public int IdContabilidad { get; set; }
    }
}
