using System.Collections.Generic;
using System.Linq;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Application.Shifts
{
	public class EightyHoursPerWeekValidator : IShiftValidator
	{
		public IEnumerable<ShiftValidationResult> ValidateShifts(IEnumerable<Shift> shifts)
		{
			var shiftList = shifts.ToList();
			var totalHours = shiftList.Select(x => (x.End - x.Begin).TotalHours).Sum();
			if(totalHours / 4 > 80)
			{
				return new List<ShiftValidationResult>
				{
					new ShiftValidationResult
					{
						Description = "One or more duty shift week averaged over 80 hours.",
						ViolatingShifts = shiftList,
						ErrorType = ShiftValidationErrorType.EightyHourPerWeek
					}
				};
			}

			return new List<ShiftValidationResult>();
		}
	}
}