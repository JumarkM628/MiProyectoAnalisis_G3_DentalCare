using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolucionLogin.UI.identity.Startup))]
namespace SolucionLogin.UI.identity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
