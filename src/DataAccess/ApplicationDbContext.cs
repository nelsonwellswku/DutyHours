using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.DataAccess
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Shift> Shifts { get; set; }

		public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
		{
		}
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Shift>()
				.HasRequired(x => x.User);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(x => x.Shifts);

			base.OnModelCreating(modelBuilder);
		}
	}
}