using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HyperlinksAndButtonsExample.Startup))]
namespace HyperlinksAndButtonsExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
