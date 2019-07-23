using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AddEditRecordExample.Startup))]
namespace AddEditRecordExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
