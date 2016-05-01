using System.Collections.Generic;
using Octogami.DutyHours.Application.Shifts;

namespace Octogami.DutyHours.Web.Models
{
	public class AnalysisViewModel
	{
		public string UserName { get; set; }
		public IEnumerable<ShiftValidationResult> ValidationResults { get; set; }
	}
}