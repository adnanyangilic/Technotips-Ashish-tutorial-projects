using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DifferenceBetweenPartialAndRenderPartial.Startup))]
namespace DifferenceBetweenPartialAndRenderPartial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
