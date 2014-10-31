using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BootstrapSandbox.Startup))]
namespace BootstrapSandbox
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
