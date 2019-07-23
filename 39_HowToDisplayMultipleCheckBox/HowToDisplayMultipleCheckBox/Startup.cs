using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HowToDisplayMultipleCheckBox.Startup))]
namespace HowToDisplayMultipleCheckBox
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
