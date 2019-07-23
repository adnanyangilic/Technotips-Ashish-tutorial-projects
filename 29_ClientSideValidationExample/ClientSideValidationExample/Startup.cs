using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientSideValidationExample.Startup))]
namespace ClientSideValidationExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
