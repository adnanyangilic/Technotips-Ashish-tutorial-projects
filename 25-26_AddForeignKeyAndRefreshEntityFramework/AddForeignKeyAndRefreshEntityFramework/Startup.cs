using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AddForeignKeyAndRefreshEntityFramework.Startup))]
namespace AddForeignKeyAndRefreshEntityFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
