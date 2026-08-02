using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.AccesoADatos;

namespace DentaCare.LogicaDeNegocio.Reporteria.Bitacora
{
    public class BitacoraLN
    {
        public void RegistrarVisualizacion(string modulo, string accion, string descripcion, string nombreUsuario)
        {
            using (var ctx = new Contexto())
            {
                var entidad = new DentalCare.AccesoADatos.Entidades.Bitacora.BitacoraEntidad
                {
                    Modulo = modulo,
                    Accion = accion,
                    Descripcion = descripcion,
                    NombreUsuario = nombreUsuario,
                    FechaHora = DateTime.Now
                };
                ctx.Bitacoras.Add(entidad);
                ctx.SaveChanges();
            }
        }
    }
}
