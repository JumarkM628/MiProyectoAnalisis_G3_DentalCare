using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.Modelo.Archivo
{
    public class ArchivoClinicoDto
    {
        public int Id { get; set; }
        public int ExpedienteId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreArchivo { get; set; }
        public int TipoArchivoId { get; set; }
        public string FormatoArchivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCarga { get; set; }
        public string RutaArchivo { get; set; }
    }
}
