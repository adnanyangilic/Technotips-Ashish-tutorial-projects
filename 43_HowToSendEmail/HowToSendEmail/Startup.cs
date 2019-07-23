using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HowToSendEmail.Startup))]
namespace HowToSendEmail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
