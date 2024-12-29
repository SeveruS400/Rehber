using Entities.Dtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ISurveyQuestionService
    {
        IEnumerable<SurveyQuestion> GetAllSurveyQuestion(bool trackChanges,int Id);
        void UpdateSurveyQuestion(SurveyQuestion surveyQuestion);
        int GetLastYesNoSurveyID();
    }
}
