using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Act11.Startup))]
namespace Act11
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
