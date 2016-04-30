using System;
using System.Collections.Generic;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Tests.Application.Shifts
{
	public static class ShiftsTestData
	{
		public static List<Shift> ValidShifts
		{
			get
			{
				var beginningOfFirstShift = new DateTimeOffset(2000, 01, 01, 8, 0, 0, TimeSpan.Zero);
				var shiftOne = new Shift
				{
					Begin = beginningOfFirstShift,
					End = beginningOfFirstShift.AddHours(8)
				};
				var shiftTwo = new Shift
				{
					Begin = shiftOne.End.AddHours(8),
					End = shiftOne.End.AddHours(16)
				};
				var shiftThree = new Shift
				{
					Begin = shiftTwo.End.AddHours(8),
					End = shiftTwo.End.AddHours(16)
				};

				var shifts = new List<Shift> {shiftOne, shiftTwo, shiftThree};
				return shifts;
			}
		}
	}
}