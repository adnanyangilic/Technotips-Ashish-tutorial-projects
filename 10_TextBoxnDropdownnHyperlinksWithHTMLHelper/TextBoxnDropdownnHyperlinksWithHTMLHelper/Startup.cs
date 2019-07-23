using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TextBoxnDropdownnHyperlinksWithHTMLHelper.Startup))]
namespace TextBoxnDropdownnHyperlinksWithHTMLHelper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
