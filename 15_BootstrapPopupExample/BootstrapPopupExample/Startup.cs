using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BootstrapPopupExample.Startup))]
namespace BootstrapPopupExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
