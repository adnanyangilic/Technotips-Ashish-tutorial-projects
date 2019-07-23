using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DividePageWithBootstrapGridSystem.Startup))]
namespace DividePageWithBootstrapGridSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
