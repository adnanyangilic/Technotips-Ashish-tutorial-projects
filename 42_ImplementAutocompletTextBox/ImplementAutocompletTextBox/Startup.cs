using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImplementAutocompletTextBox.Startup))]
namespace ImplementAutocompletTextBox
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
