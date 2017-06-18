using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FourYears.Startup))]
namespace FourYears
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
