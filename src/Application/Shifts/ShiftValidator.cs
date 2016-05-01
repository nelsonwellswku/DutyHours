using System.Collections.Generic;
using System.Linq;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Application.Shifts
{
	public class ShiftValidator
	{
		private readonly IEnumerable<IShiftValidator> _validators;

		public ShiftValidator(IEnumerable<IShiftValidator> validators)
		{
			_validators = validators;
		}

		public IEnumerable<ShiftValidationResult> ValidateShifts(IEnumerable<Shift> shifts)
		{
			var shiftList = shifts.ToList();
			return shiftList.Any() ?
				_validators.SelectMany(x => x.ValidateShifts(shiftList)) :
				new List<ShiftValidationResult>();
		}
	}
}