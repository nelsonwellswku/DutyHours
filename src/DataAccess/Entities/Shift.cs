using System;

namespace Octogami.DutyHours.DataAccess.Entities
{
	public class Shift
	{
		public int ShiftId { get; set; }

		public DateTimeOffset Begin { get; set; }
		public DateTimeOffset End { get; set; }

		public ApplicationUser User { get; set; }
	}
}
