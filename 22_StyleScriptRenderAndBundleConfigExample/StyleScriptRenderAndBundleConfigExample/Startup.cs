using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StyleScriptRenderAndBundleConfigExample.Startup))]
namespace StyleScriptRenderAndBundleConfigExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
