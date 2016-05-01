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
	public class TwentyFourHourDayOffValidatorTests
	{
		private TwentyFourHourDayOffValidator Validator { get; set; }

		[SetUp]
		public void SetUp()
		{
			Validator = new TwentyFourHourDayOffValidator();
		}

		[Test]
		public void HasDayOff_HappyPath()
		{
			// Arrange
			var shifts = ShiftsTestData.ValidShifts;

			// Act
			var result = Validator.ValidateShifts(shifts);

			// Assert
			result.Should().BeEmpty();
		}

		[Test]
		public void MissingDayOff()
		{
			// Arrange
			var shifts = new List<Shift>
			{
				new Shift {ShiftId = 1, Begin = new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 8, 0, 0, 0, TimeSpan.Zero)},
				new Shift {ShiftId = 2, Begin = new DateTimeOffset(2000, 1, 9, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 15, 0, 0, 0, TimeSpan.Zero)},
				new Shift {ShiftId = 3, Begin = new DateTimeOffset(2000, 1, 16, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 22, 0, 0, 0, TimeSpan.Zero)},
				new Shift {ShiftId = 4, Begin = new DateTimeOffset(2000, 1, 23, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 29, 0, 0, 0, TimeSpan.Zero)}
			};

			// Act
			var result = Validator.ValidateShifts(shifts);

			// Assert
			var singleResult = result.Single();
			singleResult.ShouldBeEquivalentTo(new ShiftValidationResult
			{
				Description = "There is no 24 hour day off within the given time period.",
				ViolatingShifts = shifts,
				ErrorType = ShiftValidationErrorType.TwentyFourHourDayOff
			});
		}
	}
}
