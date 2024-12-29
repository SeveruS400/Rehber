using rehber.Models;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Services.Implementations;
using Entities.Dtos;
using Azure;

namespace rehber.Controllers
{
	public class SurveyController : Controller
	{
		private readonly IServiceManager _manager;

		public SurveyController(IServiceManager manager)
		{
			_manager = manager;
		}

		// GET: Display Survey Form
		public IActionResult Index()
		{
			IEnumerable<Survey> Surveys = _manager.SurveyService.GetAllSurveys(false);
			return View(Surveys);
		}
       

    }
}
