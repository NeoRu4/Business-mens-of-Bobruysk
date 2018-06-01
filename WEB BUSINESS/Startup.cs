using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEB_BUSINESS.Startup))]
namespace WEB_BUSINESS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
