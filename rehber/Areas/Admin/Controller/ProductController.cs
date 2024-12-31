using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using rehber.Models;
using Services.Contracts;
using System.ComponentModel;
using System.Diagnostics;

namespace rehber.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _serviceManager;
		private readonly IMapper _mapper;
		public ProductController(IServiceManager serviceManager,IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
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
        #region Creat
        public IActionResult Create()
        {
            ViewBag.EducationStatus = _serviceManager.EducationStatus.GetAllEducationStatus(false);
            ViewBag.Category = _serviceManager.CategoryService.GetAllCategories(false);
            ViewBag.Referance = _serviceManager.ReferanceService.GetAllReferances(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] ProductDtoForInsertion productDto, string? NewCategoryName, string? NewReferanceName)
        {
            if (!string.IsNullOrEmpty(NewCategoryName))
            {
                // Yeni kategoriyi ekle
                var newCategory = new Categories
                {
                    CategoryName = NewCategoryName
                };
                _serviceManager.CategoryService.AddCategory(newCategory);

                // Yeni kategori ID'sini productDto'ya ata
                productDto.CategoryId = newCategory.Id;
            }
            if (!string.IsNullOrEmpty(NewReferanceName))
            {
                // Yeni kategoriyi ekle
                var newReferance = new Referance
                {
                    ReferanceName = NewReferanceName
                };
                _serviceManager.ReferanceService.AddReferance(newReferance);

                // Yeni kategori ID'sini productDto'ya ata
                productDto.ReferanceId = newReferance.Id;
            }
            productDto.ConnStatus = false;
            _serviceManager.ProductService.CreateProduct(productDto);
            return RedirectToAction("Index");

        }
        #endregion

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
            var lastNote = _serviceManager.NotesService.GetLastNoteByUserId(product.Id);
            var requestSuggestions = _serviceManager.RequestSuggestionsService.GetAllRequestSuggestionsByProductId(product.Id);
            return View(new ProductViewModel()
            {
                Product = product,
                Categories = categories,
                EducationStatus = EducationStatus,
                Referance = Referance,
                Survey = Surveys,
                AnsweredSurveyIds = answeredSurveyIds,
                LastAnsweredSurvey = lastAnsweredSurveyViewModel,
                RequestSuggestions = requestSuggestions,
                Notes = lastNote
            });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductDtoForUpdate productDto)
        {
            _serviceManager.ProductService.UpdateProduct(productDto);
            return RedirectToAction("Index");
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

        #region Suggestions
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
        [HttpPost]
        public IActionResult CreateNote(Notes note)
        {
            if (string.IsNullOrWhiteSpace(note.Suggestion))
            {
                // Öneri boşsa hata mesajı
                TempData["Error"] = "Not metni boş olamaz.";
                return RedirectToAction("Index"); 
            }

            note.RequestCreateTime = DateTime.Now; 
            note.RequestCreatorId = User.Identity.Name;

			var product = _serviceManager.ProductService.GetOneProduct(note.ProductId,false);
			if (product != null)
			{
				product.ConnStatus = true;
				var productDto = _mapper.Map<ProductDtoForUpdate>(product);
				_serviceManager.ProductService.UpdateProduct(productDto);
			}

			_serviceManager.NotesService.AddNotes(note);
            TempData["Success"] = "Öneriniz başarıyla kaydedildi!";
            return RedirectToAction("Index"); 
        }
        
        [HttpPost]
        public IActionResult EditSuggestions(int suggestionId, string reportText)
        {
            var ReguestTerminatedId = User.Identity.Name;
            _serviceManager.RequestSuggestionsService.UpdateRequestSuggestions(suggestionId, reportText,ReguestTerminatedId);
            return RedirectToAction("Index");
        }
        #endregion

        [HttpPost]
        public IActionResult UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Lütfen bir Excel dosyası seçin.");
            }

            var products = new List<Products>();

            // Excel dosyasını okuma
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // İlk sayfa
                    int rowCount = worksheet.Dimension.End.Row;

                    for (int row = 2; row <= rowCount; row++) // Başlık satırını atla
                    {
                        var product = new Products
                        {
                            Name = worksheet.Cells[row, 1].Text,
                            Address = worksheet.Cells[row, 2].Text,
                            PhoneNumber = worksheet.Cells[row, 5].Text,
                            ShowCase = true,
                            ConnStatus = false
                        };
                        var phoneNumberIsAvabilable = product.PhoneNumber;
                        var existingProduct = _serviceManager.ProductService.GetAllProducts(false)
                        .FirstOrDefault(p => p.PhoneNumber == phoneNumberIsAvabilable);
                        if (existingProduct == null)
                        {
                            var categoryName = worksheet.Cells[row, 6].Text; // Kategori adı
                            if (!string.IsNullOrEmpty(categoryName))
                            {
                                var category = _serviceManager.CategoryService.GetAllCategories(false).FirstOrDefault(c => c.CategoryName == categoryName);
                                if (category == null)
                                {
                                    category = new Categories { CategoryName = categoryName };
                                    _serviceManager.CategoryService.AddCategory(category);
                                    var categoryId = _serviceManager.CategoryService.GetCategoryId(categoryName);
                                    product.CategoryId = categoryId;
                                }
                                else
                                {
                                    product.CategoryId = category.Id;
                                }

                            }

                            var referanceName = worksheet.Cells[row, 3].Text;
                            if (!string.IsNullOrEmpty(referanceName))
                            {
                                var referance = _serviceManager.ReferanceService.GetAllReferances(false).FirstOrDefault(c => c.ReferanceName == referanceName);
                                if (referance == null)
                                {

                                    // Yeni kategori ekle
                                    referance = new Referance { ReferanceName = referanceName };
                                    _serviceManager.ReferanceService.AddReferance(referance);
                                    var referanceId = _serviceManager.CategoryService.GetCategoryId(categoryName);
                                    product.ReferanceId = referanceId;
                                }
                                else
                                {
                                    product.ReferanceId = referance.Id;
                                }
                            }

                            var educationStatusName = worksheet.Cells[row, 4].Text.ToLower(); // Gelen değer küçük harfe çevir
                            int? educationStatusId = null;
                            if (!string.IsNullOrEmpty(educationStatusName))
                            {
                                // Eğitim durumu eşlemesi
                                switch (educationStatusName)
                                {
                                    case "ilkokul":
                                        educationStatusId = 1; // İlkokul ID'si
                                        break;
                                    case "ortaokul":
                                        educationStatusId = 2; // Ortaokul ID'si
                                        break;
                                    case "lise":
                                        educationStatusId = 3; // Lise ID'si
                                        break;
                                    case "üniversite":
                                        educationStatusId = 4; // Ortaokul ID'si
                                        break;
                                    case "yüksek lisans":
                                        educationStatusId = 5; // Lise ID'si
                                        break;
                                    case "doktora":
                                        educationStatusId = 6; // Lise ID'si
                                        break;
                                    default:
                                        educationStatusId = -1;
                                        break;
                                }
                                product.EducationStatusId = educationStatusId;
                            }
                            if (!string.IsNullOrEmpty(product.Name) && !string.IsNullOrEmpty(product.PhoneNumber))
                            {
                                products.Add(product);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Bu telefon numarası zaten kayıtlı.");
                            continue;
                        }
                        
                    }
                }
            }

            // Ürünleri kaydetme
            _serviceManager.ProductService.SaveProducts(products);

            return RedirectToAction("Index");
        }
    }
}
