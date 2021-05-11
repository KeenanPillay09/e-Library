using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(e_Library.WebUI.Startup))]
namespace e_Library.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
