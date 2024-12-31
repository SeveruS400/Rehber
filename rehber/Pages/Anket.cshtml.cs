
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories.Contracts;

namespace rehber.Pages
{
    public class AnketModel : PageModel
    {
        private readonly IRepositoryManager _repositoryManager;

        public AnketModel(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Survey Survey { get; set; }
        public bool IsValidLink { get; set; }

        [BindProperty]
        public string[] Responses { get; set; }
        public async Task<IActionResult> OnGetAsync(int surveyId, string token)
        {
            var surveyLink = _repositoryManager.Survey.GetSurveyLink(surveyId, token);

            if (surveyLink.Result == null)
            {
                IsValidLink = false;
                return Page(); // Link geçersiz, sayfayý yükle
            }
            else
            {
                Survey = surveyLink.Result.Survey;
                IsValidLink = true;
            }

            return Page(); // Sayfayý geçerli link ile yükle
        }
    }
}
