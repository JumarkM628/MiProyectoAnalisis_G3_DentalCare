using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DentalCare.Abstraccion.AccesoADatos.Archivo;
using DentalCare.Abstraccion.LogicaDeNegocio.Archivo;
using DentalCare.Abstraccion.Modelo.Archivo;

namespace DentaCare.LogicaDeNegocio.Archivo.SubirArchivo
{
    public class ArchivoClinicoLN : IArchivoClinicoLN
    {
        private readonly IArchivoClinicoAD _archivoClinicoAD;

        private readonly string[] _formatosPermitidos = { "jpg", "jpeg", "png", "pdf" };
        private const long _tamanoMaximoBytes = 50L * 1024 * 1024; // 50 MB

        public ArchivoClinicoLN(IArchivoClinicoAD archivoClinicoAD)
        {
            _archivoClinicoAD = archivoClinicoAD;
        }

        public string SubirArchivo(ArchivoClinicoDto dto, HttpPostedFileBase archivo)
        {
            if (archivo == null || archivo.ContentLength == 0)
                return "Debe seleccionar un archivo.";

            string extension = Path.GetExtension(archivo.FileName)
                                   .TrimStart('.')
                                   .ToLower();

            if (!_formatosPermitidos.Contains(extension))
                return "El formato del archivo no es válido. Solo se permiten: jpg, png, pdf.";

            if (archivo.ContentLength > _tamanoMaximoBytes)
                return "El archivo supera el tamaño máximo permitido de 50 MB.";

            if (_archivoClinicoAD.ExisteNombreArchivo(dto.NombreArchivo, dto.ExpedienteId))
                return "Ya existe un archivo con ese nombre en el expediente.";

            string carpeta = HttpContext.Current.Server.MapPath("~/Archivos/Clinicos/");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string nombreFisico = $"{Guid.NewGuid()}_{archivo.FileName}";
            string rutaCompleta = Path.Combine(carpeta, nombreFisico);
            archivo.SaveAs(rutaCompleta);

            dto.FormatoArchivo = extension;
            dto.FechaCarga = DateTime.Now;
            dto.RutaArchivo = $"~/Archivos/Clinicos/{nombreFisico}";

            _archivoClinicoAD.GuardarArchivo(dto);

            return null; 
        }
    }
}
