using System;
using System.Collections.Generic;
using System.Linq;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Application.Shifts
{
	public class ContinuousDutyValidator : IShiftValidator
	{
		public IEnumerable<ShiftValidationResult> ValidateShifts(IEnumerable<Shift> shifts)
		{
			var invalidShifts = shifts.Where(x => x.End - x.Begin >= TimeSpan.FromHours(24)).ToList();

			return invalidShifts.Select(x => new ShiftValidationResult
			{
				Description = "One or more individual shifts were longer than 24 hours",
				ViolatingShifts = invalidShifts,
				ErrorType = ShiftValidationErrorType.TwentyFourHourContinuousDuty
			});
		}
	}
}