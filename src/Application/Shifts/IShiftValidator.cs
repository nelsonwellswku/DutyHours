using System.Collections.Generic;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Application.Shifts
{
	public interface IShiftValidator
	{
		IEnumerable<ShiftValidationResult> ValidateShifts(IEnumerable<Shift> shifts);
	}
}