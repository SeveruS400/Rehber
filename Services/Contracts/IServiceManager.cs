using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        ICategoryService CategoryService { get; }
        IReferanceService ReferanceService { get; }
        ISurveyService SurveyService { get; }
        IEducationStatusService EducationStatus { get; }
        ISurveyQuestionService SurveyQuestionService { get; }
        ISurveyResponseService SurveyResponseService { get; }
        IAuthService AuthService { get; }
        IRequestSuggestionsService RequestSuggestionsService { get; }
        INotesService NotesService { get; }
    }
}
