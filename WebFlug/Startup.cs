using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebFlug.Startup))]
namespace WebFlug
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
