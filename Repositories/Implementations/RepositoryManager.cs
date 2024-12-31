using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        #region Ctor
        private readonly DataContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEducationStatusRepository _educationStatus;
        private readonly IReferanceRepository _referanceRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyResponseRepository _surveyResponseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRequestSuggestionsRepository _requestSuggestionsRepository;
        private readonly INotesRepository _notesRepository;

        public RepositoryManager(DataContext context, 
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IEducationStatusRepository educationStatus,
            IReferanceRepository referanceRepository,
            ISurveyRepository surveyRepository,
            ISurveyQuestionRepository surveyQuestionRepository,
            ISurveyResponseRepository surveyResponseRepository,
            IUserRepository userRepository,
            IRequestSuggestionsRepository requestSuggestionsRepository,
            INotesRepository notesRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _educationStatus = educationStatus;
            _referanceRepository = referanceRepository;
            _surveyRepository = surveyRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyResponseRepository = surveyResponseRepository;
            _userRepository = userRepository;
            _requestSuggestionsRepository = requestSuggestionsRepository;
            _notesRepository = notesRepository;
        }
        #endregion


        IProductRepository IRepositoryManager.Product => _productRepository;
        public ICategoryRepository Category => _categoryRepository;
        public IEducationStatusRepository EducationStatus => _educationStatus;
        public IReferanceRepository Referance => _referanceRepository;
        public ISurveyRepository Survey => _surveyRepository;
        public ISurveyQuestionRepository SurveyQuestion => _surveyQuestionRepository;
        public ISurveyResponseRepository SurveyResponse => _surveyResponseRepository;
        public IUserRepository UserRepository => _userRepository;

        public IRequestSuggestionsRepository RequestSuggestions => _requestSuggestionsRepository;

        public INotesRepository Notes => _notesRepository;

        public DbContext CreateDbContext()
        {
            return _context;
        }

        public async void Save()
        {
            //_context.SaveChangesAsync();
            try
            {
                _context.SaveChanges();  // Burada SaveChanges çağrılıyor.
            }
            catch (Exception ex)
            {
                // Eğer hata alıyorsanız, burada hata mesajını loglayabilirsiniz.
                Console.WriteLine($"Error during saving: {ex.Message}");
                throw;
            }
        }

        DbContext IRepositoryManager.CreateDbContext()
        {
            throw new NotImplementedException();
        }


    }
}
