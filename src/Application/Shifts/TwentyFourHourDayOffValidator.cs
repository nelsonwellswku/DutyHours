using System.Collections.Generic;
using System.IO;
using System.Linq;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Application.Shifts
{
	public class TwentyFourHourDayOffValidator : IShiftValidator
	{
		private const int _totalPossibleHours = 24*7*4;
		private const int _minimumAllowedHours = 24*4;

		public IEnumerable<ShiftValidationResult> ValidateShifts(IEnumerable<Shift> shifts)
		{
			var shiftList = shifts.ToList();
			var totalShiftHours = shiftList.Select(x => (x.End - x.Begin).TotalHours).Sum();
			
			if(_totalPossibleHours - totalShiftHours <= _minimumAllowedHours)
			{
				return new List<ShiftValidationResult>()
				{
					new ShiftValidationResult
					{
						Description = "There is no 24 hour day off within the given time period.",
						ViolatingShifts = shiftList,
						ErrorType = ShiftValidationErrorType.TwentyFourHourDayOff
					}
				};
			}

			return new List<ShiftValidationResult>();
		}
	}
}