using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AttribueRoutingExample.Startup))]
namespace AttribueRoutingExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
