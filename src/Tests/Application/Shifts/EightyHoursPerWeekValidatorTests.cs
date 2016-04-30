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
	public class EightyHoursPerWeekValidatorTests
	{
		private EightyHoursPerWeekValidator Validator { get; set; }

		[SetUp]
		public void SetUp()
		{
			Validator = new EightyHoursPerWeekValidator();
		}

		[Test]
		public void EightyHoursPerWeekAverage_HappyPath()
		{
			// Arrange
			var shifts = ShiftsTestData.ValidShifts;

			// Act
			var results = Validator.ValidateShifts(shifts);

			// Assert
			results.Should().BeEmpty();
		}

		[Test]
		public void MoreThanEightyHoursAverageInASingleWeek()
		{
			// Arrange
			var shifts = new List<Shift>
			{
				new Shift {ShiftId = 1, Begin = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 5, 0, 0, 0, TimeSpan.Zero)}, // "week" 1, 120 hours
				new Shift {ShiftId = 2, Begin = new DateTimeOffset(2000, 1, 6, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 16, 0, 0, 0, TimeSpan.Zero)}, // "week" 2, 240 hours
				new Shift {ShiftId = 3, Begin = new DateTimeOffset(2000, 1, 20, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 21, 0, 0, 0, TimeSpan.Zero)}, // "week" 3, 24 hours
				new Shift {ShiftId = 4, Begin = new DateTimeOffset(2000, 1, 25, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 26, 0, 0, 0, TimeSpan.Zero)}, // "week" 4, 24 hours
			};

			// Act
			var results = Validator.ValidateShifts(shifts);

			// Assert
			var singleResult = results.Single();
			singleResult.ShouldBeEquivalentTo(new ShiftValidationResult
			{
				Description = "One or more duty shift week averaged over 80 hours.",
				ViolatingShifts = shifts,
				ErrorType = ShiftValidationErrorType.EightyHourPerWeek
			});
		}
	}
}
