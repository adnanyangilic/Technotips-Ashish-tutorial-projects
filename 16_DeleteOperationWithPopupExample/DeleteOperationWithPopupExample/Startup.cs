using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeleteOperationWithPopupExample.Startup))]
namespace DeleteOperationWithPopupExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
