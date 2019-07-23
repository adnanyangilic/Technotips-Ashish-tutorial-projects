using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PreviewImageBeforeUpload.Startup))]
namespace PreviewImageBeforeUpload
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
