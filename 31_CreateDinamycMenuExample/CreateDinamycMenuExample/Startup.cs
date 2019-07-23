using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreateDinamycMenuExample.Startup))]
namespace CreateDinamycMenuExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
