using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RMT.Startup))]
namespace RMT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
