using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HowToSendMultipleCheckboxIDs.Startup))]
namespace HowToSendMultipleCheckboxIDs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
