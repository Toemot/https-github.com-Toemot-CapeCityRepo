using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChilliApp.Startup))]
namespace ChilliApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
