using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Octogami.DutyHours.Application;
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
			builder.RegisterControllers(typeof(HomeController).Assembly);

			builder.RegisterModule<ApplicationModule>();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			app.UseAutofacMiddleware(container);

			ConfigureAuth(app);
		}
	}
}