using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DBConnectionWithEntityFramework.Startup))]
namespace DBConnectionWithEntityFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
