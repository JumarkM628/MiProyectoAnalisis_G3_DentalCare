using DentaCare.LogicaDeNegocio.Citas.RecordatorioCitaServicios;
using DentalCare.UI.Models;
// ========== AGREGADO: usings para Hangfire ==========
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
// ===================================================

namespace DentalCare.UI
{
    public partial class Startup
    {
        // Para obtener más información sobre cómo configurar la autenticación, visite https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure el contexto de base de datos, el administrador de usuarios y el administrador de inicios de sesión para usar una única instancia por solicitud
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Permitir que la aplicación use una cookie para almacenar información para el usuario que inicia sesión
            // y una cookie para almacenar temporalmente información sobre un usuario que inicia sesión con un proveedor de inicio de sesión de terceros
            // Configurar cookie de inicio de sesión
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Permite a la aplicación validar la marca de seguridad cuando el usuario inicia sesión.
                    // Es una característica de seguridad que se usa cuando se cambia una contraseña o se agrega un inicio de sesión externo a la cuenta.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Permite que la aplicación almacene temporalmente la información del usuario cuando se verifica el segundo factor en el proceso de autenticación de dos factores.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Permite que la aplicación recuerde el segundo factor de verificación de inicio de sesión, como el teléfono o correo electrónico.
            // Cuando selecciona esta opción, el segundo paso de la verificación del proceso de inicio de sesión se recordará en el dispositivo desde el que ha iniciado sesión.
            // Es similar a la opción Recordarme al iniciar sesión.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Quitar los comentarios de las siguientes líneas para habilitar el inicio de sesión con proveedores de inicio de sesión de terceros
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

            // Crear roles y usuario administrador inicial si no existen
            CreateRolesAndUsers();

            // ========== AGREGADO: Inicializar Hangfire ==========
            ConfigureHangfire(app);
            // ===================================================
        }

        // ========== AGREGADO: Método para configurar Hangfire ==========
        private void ConfigureHangfire(IAppBuilder app)
        {
            // Obtener la cadena de conexión desde Web.config (asegúrate de que exista con el nombre "DentalCareConnection")
            var connectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["Contexto"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                // Si no está definida, puedes usar la misma cadena que usa Entity Framework (ejemplo)
                // O simplemente lanzar una excepción para que sepas que falta.
                throw new Exception("La cadena de conexión 'DentalCareConnection' no está definida en Web.config.");
            }

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(connectionString);

            // Registrar el servidor de Hangfire
            app.UseHangfireServer();

            // Dashboard de Hangfire (accesible en /hangfire)
            app.UseHangfireDashboard("/hangfire");

            // Tarea recurrente cada hora para enviar recordatorios de citas
            RecurringJob.AddOrUpdate<RecordatorioCitaServicio>(
                "recordatorio-citas-24h",
                servicio => servicio.EnviarRecordatorios(),
                Cron.Hourly); // Cada hora verifica si hay citas en las próximas 24h
        }
        // ================================================================

        private void CreateRolesAndUsers()
        {
            // Usar el contexto de Identity para crear roles y un usuario admin inicial
            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new ApplicationUserManager(userStore);

                // Roles requeridas por la aplicación (incluye Recepcionista)
                string[] roles = { "Admin", "Doctor", "Asistente", "Paciente", "Recepcionista" };
                foreach (var roleName in roles)
                {
                    if (!roleManager.RoleExists(roleName))
                    {
                        roleManager.Create(new IdentityRole(roleName));
                    }
                }

                // Usuarios por defecto para cada rol (email -> password)
                var defaultUsers = new System.Collections.Generic.Dictionary<string, (string Email, string Password)>
                {
                    { "Admin", ("admin@dentalcare.local", "Admin@12345") },
                    { "Recepcionista", ("recepcionista@dentalcare.local", "Recepcionista@12345") },
                    { "Doctor", ("doctor@dentalcare.local", "Doctor@12345") },
                    { "Asistente", ("asistente@dentalcare.local", "Asistente@12345") },
                    { "Paciente", ("paciente@dentalcare.local", "Paciente@12345") }
                };

                foreach (var kv in defaultUsers)
                {
                    var role = kv.Key;
                    var email = kv.Value.Email;
                    var password = kv.Value.Password;

                    var user = userManager.FindByEmail(email);
                    if (user == null)
                    {
                        user = new ApplicationUser { UserName = email, Email = email };
                        var createResult = userManager.Create(user, password);
                        if (createResult.Succeeded)
                        {
                            userManager.AddToRole(user.Id, role);
                        }
                    }
                    else
                    {
                        if (!userManager.IsInRole(user.Id, role))
                        {
                            userManager.AddToRole(user.Id, role);
                        }
                    }
                }
            }
        }
    }
}