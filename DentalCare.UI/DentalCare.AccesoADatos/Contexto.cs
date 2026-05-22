using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DentalCare.AccesoADatos.Entidades;
using DentalCare.AccesoADatos.Entidades.Usuarios;
using DentalCare.AccesoADatos.Entidades.Estado;

namespace DentalCare.AccesoADatos
{
    public class Contexto:DbContext
    {
            public Contexto()
            {
                
            }
    
        //Usuarios
        public DbSet<UsuariosEntidad> Usuarios { get; set; }
        public DbSet<CedulaEntidad> Cedulas { get; set; }
        public DbSet<TelefonoEntidad> Telefonos { get; set; }
        public DbSet<CorreoEntidad> Correos { get; set; }
        public DbSet<AreaUsuarioEntidad> Areas { get; set; }
        public DbSet<EspecialidadEntidad> Especialidades { get; set; }
        public DbSet<EstadoEntidad> Estados { get; set; }
        public DbSet<AspNetUserEntidad> AspNetUsers { get; set; }
    }
}
