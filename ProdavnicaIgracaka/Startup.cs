using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProdavnicaIgracaka.Startup))]
namespace ProdavnicaIgracaka
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
