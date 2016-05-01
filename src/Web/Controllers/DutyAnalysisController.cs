using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Octogami.DutyHours.Application.Shifts;
using Octogami.DutyHours.DataAccess;
using Octogami.DutyHours.Web.Models;

namespace Octogami.DutyHours.Web.Controllers
{
	public class DutyAnalysisController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly ShiftValidator _validator;

		public DutyAnalysisController(ApplicationDbContext db, ShiftValidator validator)
		{
			_db = db;
			_validator = validator;
		}

		// GET: DutyAnalysis/ByUser?username={username}
		public ActionResult ByUser(string username)
		{
			var shifts = _db.Shifts
				.Where(x => x.User.UserName == username)
				.OrderBy(x => x.Begin);

			var validationResults = _validator.ValidateShifts(shifts);

			var viewModel = new AnalysisViewModel
			{
				UserName = username,
				ValidationResults = validationResults
			};

			return View(viewModel);
		}
	}
}