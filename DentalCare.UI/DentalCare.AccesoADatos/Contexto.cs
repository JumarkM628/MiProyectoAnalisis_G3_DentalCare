using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.AccesoADatos.Entidades;
using DentalCare.AccesoADatos.Entidades.Archivo;
using DentalCare.AccesoADatos.Entidades.Bitacora;
using DentalCare.AccesoADatos.Entidades.Estado;
using DentalCare.AccesoADatos.Entidades.Expediente;
using DentalCare.AccesoADatos.Entidades.Expedientes;
using DentalCare.AccesoADatos.Entidades.Tratamiento;
using DentalCare.AccesoADatos.Entidades.Usuarios;

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
        public DbSet<AspNetUserRolEntidad> AspNetUserRoles { get; set; }
        public DbSet<AspNetRoles> AspNetRoles { get; set; }

        //Expediente
        public DbSet<ExpedienteEntidad> Expedientes { get; set; }
        public DbSet<ConsentimientoEntidad> Consentimientos { get; set; }
        public DbSet<UsuarioExpedienteEntidad> UsuarioExpedientes { get; set; }

        // Tratamiento
        public DbSet<PlanTratamientoEntidad> PlanesTratamiento { get; set; }

        // Alertas
        public DbSet<AlertaMedicaEntidad> Alertas { get; set; }

        // Archivos Clínicos
        public DbSet<ArchivoClinicoEntidad> ArchivosClinicos { get; set; }

        // Bitácora
        public DbSet<BitacoraEntidad> Bitacoras { get; set; }
    }
}
