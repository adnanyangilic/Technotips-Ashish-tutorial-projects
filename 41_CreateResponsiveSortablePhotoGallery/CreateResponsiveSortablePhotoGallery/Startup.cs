using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreateResponsiveSortablePhotoGallery.Startup))]
namespace CreateResponsiveSortablePhotoGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
