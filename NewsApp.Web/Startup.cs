using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsApp.Web.Startup))]
namespace NewsApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
