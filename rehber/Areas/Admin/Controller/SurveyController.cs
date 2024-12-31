using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace rehber.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> CreateSurvey(Survey model)
        {
            var username = _userManager.GetUserName(User);
            model.CreatedUser = username;
            model.LastEditUser = username;

            // Survey'i asenkron olarak ekliyoruz
            await _manager.SurveyService.AddSurvey(model);

            // Asenkron olarak product bilgilerini alıyoruz
            var user = await _manager.ProductService.GetOneProductAsync(1, false);

            // Asenkron olarak benzersiz linki oluşturuyoruz
            var link = await GenerateUniqueLinkAsync(model, user);

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
        public async Task<string> GenerateUniqueLinkAsync(Survey survey, Products user)
        {
            var token = Guid.NewGuid().ToString(); // Benzersiz bir token oluşturuluyor
            var link = $"{Request.Scheme}://{Request.Host}/anket?surveyId={survey.Id}&token={token}";

            var surveyLink = new SurveyLink
            {
                SurveyId = survey.Id,
                UserId = user.Id,
                UniqueLink = token,
                IsUsed = false
            };

            // Asenkron olarak SurveyLink kaydediyoruz
            await _manager.SurveyService.GenerateUniqueLinkAsync(surveyLink);

            return link;
        }

    }
}