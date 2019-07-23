using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreateLoginExample.Startup))]
namespace CreateLoginExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
