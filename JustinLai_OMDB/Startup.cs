using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JustinLai_OMDB.Startup))]
namespace JustinLai_OMDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
