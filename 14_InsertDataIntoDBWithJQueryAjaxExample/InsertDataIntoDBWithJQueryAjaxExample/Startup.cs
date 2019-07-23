using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InsertDataIntoDBWithJQueryAjaxExample.Startup))]
namespace InsertDataIntoDBWithJQueryAjaxExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
