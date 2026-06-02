using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DentalCare.Abstraccion.Modelo.Archivo;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Archivo
{
    public interface IArchivoClinicoLN
    {
        string SubirArchivo(ArchivoClinicoDto dto, HttpPostedFileBase archivo);
    }
}
