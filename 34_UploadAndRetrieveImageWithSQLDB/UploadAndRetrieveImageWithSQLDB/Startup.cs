using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UploadAndRetrieveImageWithSQLDB.Startup))]
namespace UploadAndRetrieveImageWithSQLDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
