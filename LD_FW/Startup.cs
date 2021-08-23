using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LD_FW.Startup))]
namespace LD_FW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
