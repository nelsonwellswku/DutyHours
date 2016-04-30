using Microsoft.AspNet.Identity.EntityFramework;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.DataAccess
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}
	}
}