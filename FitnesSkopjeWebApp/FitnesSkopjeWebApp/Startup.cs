using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitnesSkopjeWebApp.Startup))]
namespace FitnesSkopjeWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
