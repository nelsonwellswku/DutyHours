using System;
using System.Collections.Generic;
using System.Linq;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Application.Shifts
{
	public class HoursOffBetweenShiftsValidator : IShiftValidator
	{
		public IEnumerable<ShiftValidationResult> ValidateShifts(IEnumerable<Shift> shifts)
		{
			var invalidShifts = new List<Shift>();
			var _ = shifts.Aggregate((current, next) =>
			{
				if(next.Begin - current.End < TimeSpan.FromHours(8))
				{
					invalidShifts.Add(current);
				}

				return next;
			});

			return invalidShifts.Select(x => new ShiftValidationResult
			{
				Description = "The ending of one shift was less than 8 hours from the beginning of another shift.",
				ViolatingShifts = invalidShifts,
				ErrorType = ShiftValidationErrorType.EightHoursOffBetweenShifts
			});
		}
	}
}
