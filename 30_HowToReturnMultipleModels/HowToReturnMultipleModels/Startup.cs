using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HowToReturnMultipleModels.Startup))]
namespace HowToReturnMultipleModels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
