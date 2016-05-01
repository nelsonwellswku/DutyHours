using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.DataAccess.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			// Add some test users
			var john = new ApplicationUser {UserName = "John"};
			var patty = new ApplicationUser {UserName = "Patty"};
			var mike = new ApplicationUser {UserName = "Mike"};
			context.Users.AddOrUpdate(p => p.UserName,
				john,
				patty,
				mike
			);

			// Add some shifts that would throw some analysis errors
			context.Shifts.AddOrUpdate(x => x.Begin,
			
				// for john
				new Shift
				{
					Begin = new DateTimeOffset(2000, 1, 1, 8, 0, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2000, 1, 1, 17, 0, 0, 0, TimeSpan.Zero),
					User = john
				},
				new Shift
				{
					Begin = new DateTimeOffset(2000, 1, 2, 8, 0, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2000, 1, 3, 9, 0, 0, 0, TimeSpan.Zero),
					User = john
				},
				new Shift
				{
					Begin = new DateTimeOffset(2000, 1, 4, 8, 0, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2000, 1, 4, 17, 0, 0, 0, TimeSpan.Zero),
					User = john
				},
				new Shift
				{
					Begin = new DateTimeOffset(2000, 1, 5, 8, 0, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2000, 1, 5, 17, 0, 0, 0, TimeSpan.Zero),
					User = john
				}
			);
		}
	}
}