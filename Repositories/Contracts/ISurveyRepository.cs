using Entities.Models;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ISurveyRepository : IRepositoryBase<Survey>
    {
        Task AddSurvey(Survey survey);
        IEnumerable<Survey> GetAllSurveys(bool trackChanges);
        Survey GetSurveyById(bool trackChanges, int Id);
        void GenerateUniqueLink(SurveyLink surveyLink);
        Task<SurveyLink> GetSurveyLink(int surveyId, string token);
    }
}
