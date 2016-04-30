using Microsoft.AspNet.Identity.EntityFramework;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.DataAccess.Identity
{
	public class ApplicationUserStore : UserStore<ApplicationUser>
	{
		public ApplicationUserStore(ApplicationDbContext context) : base(context)
		{
		}
	}
}