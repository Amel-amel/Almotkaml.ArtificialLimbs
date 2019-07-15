using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Almotkaml.ArtificialLimbs.Startup))]
namespace Almotkaml.ArtificialLimbs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
