using Autofac;

namespace Octogami.DutyHours.Application
{
	public class ApplicationModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var assembly = typeof(ApplicationModule).Assembly;
			builder.RegisterAssemblyTypes(assembly)
				.AsImplementedInterfaces();
		}
	}
}