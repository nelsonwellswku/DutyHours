using System.Collections.Generic;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Application.Shifts
{
	public class ShiftValidationResult
	{
		public string Description { get; set; }
		public IEnumerable<Shift> ViolatingShifts { get; set; }
		public ShiftValidationErrorType ErrorType { get; set; }
	}

	public enum ShiftValidationErrorType
	{
		Unknown = 0,
		EightyHourPerWeek,
		TwentyFourHourDayOff,
		TwentyFourHourContinuousDuty,
		EightHoursOffBetweenShifts
	}
}
