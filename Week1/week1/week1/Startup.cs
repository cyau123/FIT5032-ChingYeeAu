using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(week1.Startup))]
namespace week1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
