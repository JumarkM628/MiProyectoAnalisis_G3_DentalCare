using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Archivo;

namespace DentalCare.Abstraccion.AccesoADatos.Archivo
{
    public interface IArchivoClinicoAD
    {
        bool ExisteNombreArchivo(string nombreArchivo, int expedienteId);
        bool GuardarArchivo(ArchivoClinicoDto dto);
    }
}
