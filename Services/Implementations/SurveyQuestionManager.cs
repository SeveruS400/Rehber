using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class SurveyQuestionManager : ISurveyQuestionService
    {
        private readonly IRepositoryManager _manager;

        public SurveyQuestionManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
        public IEnumerable<SurveyQuestion> GetAllSurveyQuestion(bool trackChanges, int Id)
        {
            return _manager.SurveyQuestion.GetAllSurveyQuestionBySurveyId(trackChanges, Id);
        }

        public int GetLastYesNoSurveyID()
        {
            return _manager.SurveyQuestion.GetLastYesNoSurveyID();
        }

        public void UpdateSurveyQuestion(SurveyQuestion surveyQuestion)
        {
            
            _manager.SurveyQuestion.UpdateAsync(surveyQuestion.Id, surveyQuestion).Wait();
            _manager.Save();
        }

    }
}
