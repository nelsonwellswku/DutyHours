using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Octogami.DutyHours.Application.Shifts;
using Octogami.DutyHours.DataAccess.Entities;

namespace Octogami.DutyHours.Tests.Application.Shifts
{
	public class HoursOffBetweenShiftsValidatorTests
	{
		private HoursOffBetweenShiftsValidator Validator;

		[SetUp]
		public void SetUp()
		{
			Validator = new HoursOffBetweenShiftsValidator();
		}

		[Test]
		public void NoShiftsThatHaveLessThan8HoursBetweenThem()
		{
			// Arrange
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

			// Act
			var results = Validator.ValidateShifts(shifts);

			// Assert
			results.Should().BeEmpty();
		}

		[Test]
		public void SevenHoursBetweenSecondAndThirdShift()
		{
			// Arrange
			var beginningOfFirstShift = new DateTimeOffset(2000, 01, 01, 8, 0, 0, TimeSpan.Zero);
			var shiftOne = new Shift
			{
				ShiftId = 1,
				Begin = beginningOfFirstShift,
				End = beginningOfFirstShift.AddHours(8)
			};
			var shiftTwo = new Shift
			{
				ShiftId = 2,
				Begin = shiftOne.End.AddHours(8),
				End = shiftOne.End.AddHours(16)
			};
			var shiftThree = new Shift
			{
				ShiftId = 3,
				Begin = shiftTwo.End.AddHours(7),
				End = shiftTwo.End.AddHours(16)
			};

			var shifts = new List<Shift> { shiftOne, shiftTwo, shiftThree };

			// Act
			var results = Validator.ValidateShifts(shifts);

			// Assert
			var singleResult = results.Single();
			singleResult.ShouldBeEquivalentTo(new ShiftValidationResult
			{
				Description = "The ending of one shift was less than 8 hours from the beginning of another shift.",
				ViolatingShifts = new List<Shift>() { new Shift {  ShiftId = 2, Begin = shiftTwo.Begin, End = shiftTwo.End} },
				ErrorType = ShiftValidationErrorType.EightHoursOffBetweenShifts
			});
		}
	}
}
