using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace rehber.Areas.Editor.Controllers
{
	[Area("Editor")]
	[Authorize(Roles = "Editor")]
	public class SurveyController : Controller
	{
		private readonly IServiceManager _manager;
        private readonly UserManager<Users> _userManager;

        public SurveyController(IServiceManager manager, UserManager<Users> userManager)
		{
			_manager = manager;
			_userManager = userManager;
		}

		// GET: Display Survey Form
		public IActionResult Index()
		{
			IEnumerable<Survey> Surveys = _manager.SurveyService.GetAllSurveys(false);
			return View(Surveys);
		}
		#region Create
		public IActionResult CreateSurvey()
		{
			return View();
		}
		[HttpPost]
		public IActionResult CreateSurvey(Survey model)
		{
			var username = _userManager.GetUserName(User);
			model.CreatedUser = username;
			model.LastEditUser = username;
			_manager.SurveyService.AddSurvey(model);
			return RedirectToAction("Index");
		}
		#endregion

		#region Edit
		public IActionResult Edit(int Id)
		{
			//var survey = _manager.SurveyService.GetSurveyById(false, Id);
			var survetQuestions = _manager.SurveyQuestionService.GetAllSurveyQuestion(false, Id);
			return View(survetQuestions);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(SurveyQuestion model)
		{
            var username = _userManager.GetUserName(User);
            
            _manager.SurveyQuestionService.UpdateSurveyQuestion(model);
            _manager.SurveyService.UpdateSurveyEditTime(model.SurveyId, username);
			return RedirectToAction("Edit", new { Id = model.SurveyId });
		}
		#endregion

	}
}
