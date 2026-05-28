using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DentalCare.UI.Startup))]
namespace DentalCare.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
