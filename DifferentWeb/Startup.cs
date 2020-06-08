using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DifferentWeb.Startup))]
namespace DifferentWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
