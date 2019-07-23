using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViewBagnViewDatanTempDataExample.Startup))]
namespace ViewBagnViewDatanTempDataExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
