using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Archivo;
using DentalCare.AccesoADatos.Entidades.Archivo;

namespace DentalCare.AccesoADatos.Archivo.SubirArchivo
{
    public class ArchivoClinicoAD
    {
        private readonly Contexto _contexto;

        public ArchivoClinicoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public bool ExisteNombreArchivo(string nombreArchivo, int expedienteId)
        {
            return _contexto.ArchivosClinicos.Any(a => a.RUTA_ARCHIVO == nombreArchivo);
        }

        public bool GuardarArchivo(ArchivoClinicoDto dto)
        {
            var entidad = new ArchivoClinicoEntidad
            {
                ID_TIPO_ARCHIVO = dto.TipoArchivoId,
                RUTA_ARCHIVO = dto.RutaArchivo,
                FECHA = dto.FechaCarga,
                ID_ESTADO = 1
            };

            _contexto.ArchivosClinicos.Add(entidad);
            _contexto.SaveChanges();
            return true;
        }
    }
}
