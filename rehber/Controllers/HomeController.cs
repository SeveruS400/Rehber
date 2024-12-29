using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using rehber.Models;
using Services.Contracts;
using System;
using System.Diagnostics;

namespace rehber.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly UserManager<Users> _userManager;

        public HomeController(ILogger<HomeController> logger, IServiceManager serviceManager, UserManager<Users> userManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _userManager = userManager;
        }

        public IActionResult Index(int selectedSurveyId)
        {

            if (selectedSurveyId == 0)
            {
                selectedSurveyId = _serviceManager.SurveyQuestionService.GetLastYesNoSurveyID();
            }

            var responses = _serviceManager.SurveyResponseService.GetAllYesNoResponsesBySurveyID(selectedSurveyId);

            var groupedData = responses
                .GroupBy(response => response.Question.Text) // Sorulara göre gruplama
                .Select(group => new QuestionResponseStatisticsViewModel
                {
                    QuestionText = group.Key,
                    YesCount = group.Count(response => response.ResponseText == "Yes"),
                    NoCount = group.Count(response => response.ResponseText == "No"),
                    YesPercentage = group.Count(response => response.ResponseText == "Yes") * 100.0 / group.Count(),
                    NoPercentage = group.Count(response => response.ResponseText == "No") * 100.0 / group.Count()
                }).ToList();

            // Tüm YesNo tipi soruları olan anketleri getir
            var yesNoSurveys = _serviceManager.SurveyService.GetAllSurveys(false)
                .Where(survey => _serviceManager.SurveyQuestionService
                    .GetAllSurveyQuestion(false, survey.Id)
                    .Any(question => question.AnswerType == "YesNo"))
                .Select(survey => new SelectListItem
                {
                    Value = survey.Id.ToString(),
                    Text = survey.Title
                }).ToList();

            ViewBag.YesNoSurveys = yesNoSurveys; // Anketleri ViewBag ile gönder
            ViewBag.SelectedSurveyId = selectedSurveyId; // Seçilen anketi ViewBag'e ekleyelim

            return View(groupedData);
        }



        public IActionResult Privacy()
        {
            var suggestions = _serviceManager.RequestSuggestionsService.GetAllRequestSuggestions();
            return View(suggestions);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _serviceManager.CategoryService.GetAllCategories(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] ProductDtoForInsertion productDto)
        {

            _serviceManager.ProductService.CreateProduct(productDto);
            return RedirectToAction("Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}