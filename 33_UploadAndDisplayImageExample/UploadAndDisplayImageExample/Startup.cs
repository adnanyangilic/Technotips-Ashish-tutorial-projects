using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UploadAndDisplayImageExample.Startup))]
namespace UploadAndDisplayImageExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
