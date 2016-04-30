using Autofac;
using Microsoft.AspNet.Identity;
using Octogami.DutyHours.DataAccess.Entities;
using Octogami.DutyHours.DataAccess.Identity;

namespace Octogami.DutyHours.DataAccess
{
	public class DataAccessModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
			builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerLifetimeScope();
			builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope();
			builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerLifetimeScope();
		}
	}
}
