using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gcss.Startup))]
namespace gcss
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
