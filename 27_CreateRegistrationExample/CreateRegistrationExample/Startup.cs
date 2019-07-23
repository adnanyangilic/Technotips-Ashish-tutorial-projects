using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreateRegistrationExample.Startup))]
namespace CreateRegistrationExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
