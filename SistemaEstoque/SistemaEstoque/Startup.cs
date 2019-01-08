using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaEstoque.Startup))]
namespace SistemaEstoque
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
