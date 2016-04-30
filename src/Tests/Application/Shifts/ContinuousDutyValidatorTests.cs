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
	public class ContinuousDutyValidatorTests
	{
		private ContinuousDutyValidator Validator { get; set; }

		[SetUp]
		public void SetUp()
		{
			Validator = new ContinuousDutyValidator();
		}

		[Test]
		public void NoShiftsLongerThan24Hours_HappyPath()
		{
			// Arrange
			var shifts = ShiftsTestData.ValidShifts;

			// Act
			var results = Validator.ValidateShifts(shifts);

			// Assert
			results.Should().BeEmpty();
		}

		[Test]
		public void OneShiftLastsLongerThan24Hours()
		{
			// Arrange
			var shifts = new List<Shift>
			{
				new Shift { ShiftId = 1, Begin = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 1, 12, 0, 0, TimeSpan.Zero)},
				new Shift { ShiftId = 2, Begin = new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 3, 0, 0, 0, TimeSpan.Zero)}
			};

			// Act
			var results = Validator.ValidateShifts(shifts);

			// Assert
			var singleResult = results.Single();
			singleResult.ShouldBeEquivalentTo(new ShiftValidationResult
			{
				Description = "One or more individual shifts were longer than 24 hours",
				ViolatingShifts = new List<Shift>() { shifts[1]},
				ErrorType = ShiftValidationErrorType.TwentyFourHourContinuousDuty
			});
		}

		[Test]
		public void TwoShiftsLongerThan24Hours()
		{
			// Arrange
			var shifts = new List<Shift>
			{
				new Shift { ShiftId = 1, Begin = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 1, 12, 0, 0, TimeSpan.Zero)},
				new Shift { ShiftId = 2, Begin = new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 3, 0, 0, 0, TimeSpan.Zero)},
				new Shift { ShiftId = 3, Begin = new DateTimeOffset(2000, 1, 5, 0, 0, 0, TimeSpan.Zero), End = new DateTimeOffset(2000, 1, 8, 0, 0, 0, TimeSpan.Zero)},
			};

			// Act
			var results = Validator.ValidateShifts(shifts);

			// Assert
			results.SelectMany(x => x.ViolatingShifts).Select(x => x.ShiftId).Distinct().ShouldBeEquivalentTo(new[] {2, 3});
		}
	}
}
