using Entities.Models;
using Services.Contracts;

namespace Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IEducationStatusService _educationStatusService;
        private readonly IReferanceService _referanceService;
        private readonly ISurveyService _surveyService;
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly ISurveyResponseService _surveyResponseService;
        private readonly IAuthService _authService;
        private readonly IRequestSuggestionsService _requestSuggestionsService;

        public ServiceManager(IProductService productService, 
            ICategoryService categoryService,
            IEducationStatusService educationStatusService, 
            IReferanceService referanceService,
            ISurveyService surveyService,
            ISurveyQuestionService surveyQuestionService,
            ISurveyResponseService surveyResponseService,
            IAuthService authService,
            IRequestSuggestionsService requestSuggestionsService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _educationStatusService = educationStatusService;
            _referanceService = referanceService;
            _surveyService = surveyService;
            _surveyQuestionService = surveyQuestionService;
            _surveyResponseService = surveyResponseService;
            _authService = authService;
            _requestSuggestionsService = requestSuggestionsService;
        }

        public IProductService ProductService => _productService;
        public ICategoryService CategoryService => _categoryService;
        public IEducationStatusService EducationStatus => _educationStatusService;
        public IReferanceService ReferanceService => _referanceService;
        public ISurveyService SurveyService => _surveyService;

        public ISurveyQuestionService SurveyQuestionService => _surveyQuestionService;

        public ISurveyResponseService SurveyResponseService => _surveyResponseService;

        public IAuthService AuthService => _authService;

        public IRequestSuggestionsService RequestSuggestionsService => _requestSuggestionsService;
    }
}
