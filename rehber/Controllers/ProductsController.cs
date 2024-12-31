using Azure;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rehber.Models;
using Services.Contracts;

namespace rehber.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public ProductsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public IActionResult Index(ProductRequestParameters p)
        {
            var categories = _serviceManager.CategoryService.GetAllCategories(false);
            var products = _serviceManager.ProductService.GetAllProducts(false);
            var EducationStatus = _serviceManager.EducationStatus.GetAllEducationStatus(false);
            var Referance = _serviceManager.ReferanceService.GetAllReferances(false);

            var LastSurveyId = _serviceManager.SurveyService.LatestSurveyId();
            var userLastSurveyStatus = new Dictionary<int, bool>();
            if (LastSurveyId != 0)
            {
                
                foreach (var product in products)
                {
                    var lastSurveyResponse = _serviceManager.SurveyResponseService
                        .GetResponsesByUserId(product.Id)
                        .OrderByDescending(response => response.EditDate)
                        .FirstOrDefault();
                    if (lastSurveyResponse != null)
                    {
                        if (lastSurveyResponse.Question.SurveyId == LastSurveyId)
                        {
                            userLastSurveyStatus[product.Id] = lastSurveyResponse != null;
                        }
                    }
                    else
                    {
                        userLastSurveyStatus[product.Id] = false;
                    }


                }
            }
            else
            {
                foreach (var product in products)
                {
                    userLastSurveyStatus[product.Id] = false;
                }
            }

            return View(new ProductListViewModel()
            {
                Products = products,
                Categories = categories,
                EducationStatus = EducationStatus,
                Referance = Referance,
                UserLastSurveyStatus = userLastSurveyStatus
            });
        }
        //#region Creat
        //public IActionResult Create()
        //{
        //    ViewBag.EducationStatus = _serviceManager.EducationStatus.GetAllEducationStatus(false);
        //    ViewBag.Category = _serviceManager.CategoryService.GetAllCategories(false);
        //    ViewBag.Referance = _serviceManager.ReferanceService.GetAllReferances(false);
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([FromForm] ProductDtoForInsertion productDto, string? NewCategoryName, string? NewReferanceName)
        //{
        //    if (!string.IsNullOrEmpty(NewCategoryName))
        //    {
        //        // Yeni kategoriyi ekle
        //        var newCategory = new Categories
        //        {
        //            CategoryName = NewCategoryName
        //        };
        //        _serviceManager.CategoryService.AddCategory(newCategory);

        //        // Yeni kategori ID'sini productDto'ya ata
        //        productDto.CategoryId = newCategory.Id;
        //    }
        //    if (!string.IsNullOrEmpty(NewReferanceName))
        //    {
        //        // Yeni kategoriyi ekle
        //        var newReferance = new Referance
        //        {
        //            ReferanceName = NewReferanceName
        //        };
        //        _serviceManager.ReferanceService.AddReferance(newReferance);

        //        // Yeni kategori ID'sini productDto'ya ata
        //        productDto.ReferanceId = newReferance.Id;
        //    }
        //    _serviceManager.ProductService.CreateProduct(productDto);
        //    return RedirectToAction("Index");

        //}
        //#endregion

        #region Edit
        public IActionResult Edit(int Id)
        {

            var categories = _serviceManager.CategoryService.GetAllCategories(false);
            var product = _serviceManager.ProductService.GetOneProduct(Id, false);
            var EducationStatus = _serviceManager.EducationStatus.GetAllEducationStatus(false);
            var Referance = _serviceManager.ReferanceService.GetAllReferances(false);
            IEnumerable<Survey> Surveys = _serviceManager.SurveyService.GetAllSurveys(false);

            var answeredSurveyIds = _serviceManager.SurveyResponseService
                .GetResponsesByUserId(Id)
                .Select(response => response.Question.SurveyId)
                .Distinct()
                .ToHashSet();

            //// Kullanıcının son cevapladığı anketin soruları ve cevapları
            //var responses = _serviceManager.SurveyResponseService.GetResponsesByUserId(Id);

            //// Eğer kullanıcı bir anketi cevaplamışsa, en son cevapladığı anketi bul
            //var lastAnsweredResponse = responses
            //    .OrderByDescending(response => response.Id) // ID'yi kullanarak sırala
            //    .FirstOrDefault(); // En son cevabı al

            //// Eğer en son cevaplı anket bulunmuşsa
            //LastSurveyViewModel lastAnsweredSurveyViewModel = null;

            //if (lastAnsweredResponse != null)
            //{
            //    var lastSurvey = lastAnsweredResponse.Question.Survey;

            //    // Soruları ve cevapları alıyoruz
            //    var surveyQuestionsAndResponses = _serviceManager.SurveyQuestionService
            //        .GetAllSurveyQuestion(false, lastSurvey.Id)
            //        .Select(question => new QuestionAnswerViewModel
            //        {
            //            QuestionText = question.Text,
            //            Answer = question.Responses != null && question.Responses.Any(r => r.UserId == Id)
            //                ? question.Responses
            //                    .Where(r => r.UserId == Id) // Kullanıcının cevabı
            //                    .Select(r => r.ResponseText)
            //                    .FirstOrDefault() ?? "Yanıt Verilmedi"
            //                : "Yanıt Verilmedi"
            //        }).ToList();


            //    // ViewModel oluşturuyoruz
            //    lastAnsweredSurveyViewModel = new LastSurveyViewModel
            //    {
            //        SurveyId = lastSurvey.Id,
            //        SurveyTitle = lastSurvey.Title,
            //        QuestionsAndAnswers = surveyQuestionsAndResponses
            //    };
            //}
            var LastSolvedSurveyId = _serviceManager.SurveyResponseService.LastSolvedSurveyId(product.Id);
            var responses = _serviceManager.SurveyResponseService.GetResponsesWithSurveyIDByUserId(product.Id, LastSolvedSurveyId);
            var responseViewModel = responses.Select(response => new QuestionAnswerViewModel
            {
                QuestionText = response.Question.Text,
                Answer = response.ResponseText
            }).ToList();

            LastSurveyViewModel lastAnsweredSurveyViewModel = null;
            lastAnsweredSurveyViewModel = new LastSurveyViewModel
            {
                SurveyId = LastSolvedSurveyId,
                SurveyTitle = LastSolvedSurveyId.ToString(),
                QuestionsAndAnswers = responseViewModel
            };

            return View(new ProductViewModel()
            {
                Product = product,
                Categories = categories,
                EducationStatus = EducationStatus,
                Referance = Referance,
                Survey = Surveys,
                AnsweredSurveyIds = answeredSurveyIds,
                LastAnsweredSurvey = lastAnsweredSurveyViewModel
            });

        }

        #endregion

        #region Survey Submit
        public IActionResult SolveSurvey(int surveyId, int productId)
        {
            var surveyQuestions = _serviceManager.SurveyQuestionService.GetAllSurveyQuestion(false, surveyId);
            var viewModel = new SolveSurveyViewModel
            {
                SurveyId = surveyId,
                ProductId = productId,
                Questions = surveyQuestions.Select(q => new QuestionResponseViewModel
                {
                    QuestionId = q.Id,
                    Text = q.Text,
                    AnswerType = q.AnswerType,
                    Response = ""
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SubmitResponses(int surveyId, int productId, Dictionary<int, string> responses)
        {
            foreach (var response in responses)
            {
                var newResponse = new SurveyResponse
                {
                    QuestionId = response.Key,
                    UserId = productId, // Kullanıcı Id'yi temsil ediyor (örnek için)
                    ResponseText = response.Value // Kullanıcı cevabı
                };

                _serviceManager.SurveyResponseService.CreateResponse(newResponse);
            }

            return RedirectToAction("Edit", new { Id = productId }); // İşlem tamamlandıktan sonra bir listeye yönlendirebilirsiniz
        }
        #endregion

        [HttpPost]
        public IActionResult CreateSuggestions(RequestSuggestions suggestionModel)
        {
            if (string.IsNullOrWhiteSpace(suggestionModel.Suggestion))
            {
                // Öneri boşsa hata mesajı
                TempData["Error"] = "Öneri metni boş olamaz.";
                return RedirectToAction("Index"); // Geri döneceğiniz sayfa
            }

            suggestionModel.RequestCreateTime = DateTime.Now; // Oluşturma zamanı
            suggestionModel.SuggestionStatus = false; // Öneri durumu başlangıçta false
            suggestionModel.RequestCreatorId = User.Identity.Name; // Kullanıcı ID'sini al

            _serviceManager.RequestSuggestionsService.CreateRequestSuggestion(suggestionModel);

            TempData["Success"] = "Öneriniz başarıyla kaydedildi!";
            return RedirectToAction("Index"); // Geri döneceğiniz sayfa
        }

    }
}
