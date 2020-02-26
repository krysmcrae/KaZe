using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KaZe.Startup))]
namespace KaZe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
