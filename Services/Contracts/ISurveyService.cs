using Entities.Dtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ISurveyService
    {
        Task AddSurvey(Survey survey);
        Task DeleteSurvey(int Id);
        IEnumerable<Survey> GetAllSurveys(bool trackChanges);
        Survey GetSurveyById(bool trackChanges, int Id);
        void UpdateSurvey(Survey Survey);
        void UpdateSurveyEditTime(int Id, string userName);
        int LatestSurveyId();

    }
}
