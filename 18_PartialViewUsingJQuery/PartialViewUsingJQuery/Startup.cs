using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PartialViewUsingJQuery.Startup))]
namespace PartialViewUsingJQuery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
