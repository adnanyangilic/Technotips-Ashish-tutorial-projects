using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RenderBodynSecionnPage.Startup))]
namespace RenderBodynSecionnPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
