using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IntegrateDataTables.Startup))]
namespace IntegrateDataTables
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
