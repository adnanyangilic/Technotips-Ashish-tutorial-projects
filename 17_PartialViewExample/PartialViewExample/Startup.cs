using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PartialViewExample.Startup))]
namespace PartialViewExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
