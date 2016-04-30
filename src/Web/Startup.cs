using Microsoft.Owin;
using Octogami.DutyHours.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Octogami.DutyHours.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
