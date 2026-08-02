using DentalCare.AccesoADatos.Entidades;
using DentalCare.AccesoADatos.Entidades.Archivo;
using DentalCare.AccesoADatos.Entidades.Bitacora;
using DentalCare.AccesoADatos.Entidades.Catalogo;
using DentalCare.AccesoADatos.Entidades.Citas;
using DentalCare.AccesoADatos.Entidades.Estado;
using DentalCare.AccesoADatos.Entidades.Expediente;
using DentalCare.AccesoADatos.Entidades.Expedientes;
using DentalCare.AccesoADatos.Entidades.Gastos;
using DentalCare.AccesoADatos.Entidades.Odontograma;
using DentalCare.AccesoADatos.Entidades.Pago;
using DentalCare.AccesoADatos.Entidades.Procedimiento;
using DentalCare.AccesoADatos.Entidades.Producto;
using DentalCare.AccesoADatos.Entidades.Tratamientos;
using DentalCare.AccesoADatos.Entidades.Usuarios;
using System.Data.Entity;

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
        public DbSet<CatalogoTratamientoEntidad> CatalogoTratamientos { get; set; }

        // Alertas
        public DbSet<AlertaMedicaEntidad> Alertas { get; set; }

        // Archivos Clínicos
        public DbSet<ArchivoClinicoEntidad> ArchivosClinicos { get; set; }

        // Bitácora
        public DbSet<BitacoraEntidad> Bitacoras { get; set; }

        //odontograma
        public DbSet<OdontogramaEntidad> Odontogramas { get; set; }
        public DbSet<PiezaDentalEntidad> PiezasDentales { get; set; }
        public DbSet<OdontogramaDetalleEntidad> OdontogramaDetalles { get; set; }

        //Citas
        public DbSet<CitaEntidad> Citas { get; set; }
        public DbSet<MotivoCitaEntidad> MotivosCita { get; set; }
        public DbSet<UsuarioCitaEntidad> UsuarioCitas { get; set; }
        public DbSet<MotivoCancelacionEntidad> MotivoCancelacionCita { get; set; }

        //Producto
        public DbSet<ProductoEntidad> Productos { get; set; }
        public DbSet<UsoProductoEntidad> UsoProductos { get; set; }
        public DbSet<ProcedimientoEntidad> Procedimientos { get; set; }
        public DbSet<ComprasProductoEntidad> ComprasProducto { get; set; }
        public DbSet<ProveedorProductoEntidad> ProveedorProductos { get; set; }
        public DbSet<CategoriaProductoEntidad> CategoriasProducto { get; set; }
        public DbSet<ProveedorEntidad> Proveedores { get; set; }

        //Pagos
        public DbSet<MetodoPagoEntidad> MetodosPago { get; set; }
        public DbSet<AnticipoEntidad> Anticipos { get; set; }
        public DbSet<ContabilidadEntidad> Contabilidades { get; set; }
        public DbSet<ContabilidadCitaEntidad> ContabilidadCitas { get; set; }
        public DbSet<ContabilidadPacienteEntidad> ContabilidadPacientes { get; set; }

        //Gastos
        public DbSet<GastoEntidad> Gastos { get; set; }
    }
}
