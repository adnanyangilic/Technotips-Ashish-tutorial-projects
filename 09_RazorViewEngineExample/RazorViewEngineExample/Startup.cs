using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RazorViewEngineExample.Startup))]
namespace RazorViewEngineExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
