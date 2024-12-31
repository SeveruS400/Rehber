using Microsoft.EntityFrameworkCore;


namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IEducationStatusRepository EducationStatus { get; }
        IReferanceRepository Referance { get; }
        ISurveyRepository Survey { get; }
        ISurveyQuestionRepository SurveyQuestion { get; }
        ISurveyResponseRepository SurveyResponse { get; }
        IUserRepository UserRepository { get; }
        IRequestSuggestionsRepository RequestSuggestions { get; }
        INotesRepository Notes { get; }
        void Save();
        DbContext CreateDbContext();
    }
}
