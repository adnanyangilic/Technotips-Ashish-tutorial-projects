using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InsertDaaIntoDBMultipleTableExample.Startup))]
namespace InsertDaaIntoDBMultipleTableExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
