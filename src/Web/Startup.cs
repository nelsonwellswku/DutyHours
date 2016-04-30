using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Octogami.DutyHours.Application;
using Octogami.DutyHours.DataAccess;
using Octogami.DutyHours.Web;
using Octogami.DutyHours.Web.Controllers;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Octogami.DutyHours.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var builder = new ContainerBuilder();

			// Register dependent modules
			builder.RegisterModule<DataAccessModule>();
			builder.RegisterModule<ApplicationModule>();

			// Register dependencies that are specific to web request scope
			builder.RegisterControllers(typeof(HomeController).Assembly);
			builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
			builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			app.UseAutofacMiddleware(container);

			ConfigureAuth(app);
		}
	}
}