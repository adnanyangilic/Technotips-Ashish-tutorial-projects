using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkingWithMultipleTablesExample.Startup))]
namespace WorkingWithMultipleTablesExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
