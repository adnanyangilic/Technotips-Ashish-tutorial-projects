using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InsertDataIntoDBExample.Startup))]
namespace InsertDataIntoDBExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
