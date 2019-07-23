using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServerAndClientSideValidationExample.Startup))]
namespace ServerAndClientSideValidationExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
