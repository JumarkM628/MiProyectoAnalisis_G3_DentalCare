using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Archivo
{
    [Table("FIDE_ARCHIVO_TB")]
    public class ArchivoClinicoEntidad
    {
        [Key]
        public int ID_ARCHIVO { get; set; }
        public int ID_TIPO_ARCHIVO { get; set; }
        public string RUTA_ARCHIVO { get; set; }
        public DateTime? FECHA { get; set; }
        public int ID_ESTADO { get; set; }
    }
}
