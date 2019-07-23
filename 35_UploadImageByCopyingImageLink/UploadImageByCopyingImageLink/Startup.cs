using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UploadImageByCopyingImageLink.Startup))]
namespace UploadImageByCopyingImageLink
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
