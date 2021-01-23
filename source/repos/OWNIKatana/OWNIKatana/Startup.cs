using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OWNIKatana.Startup))]
namespace OWNIKatana
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
