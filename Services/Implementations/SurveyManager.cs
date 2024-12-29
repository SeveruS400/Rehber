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
    public class SurveyManager : ISurveyService
    {
        private readonly IRepositoryManager _manager;


        public SurveyManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
        public async Task AddSurvey(Survey survey)
        {
            _manager.Survey.AddSurvey(survey);
        }

        public async Task DeleteSurvey(int Id)
        {
            _manager
               .Survey
			   .DeleteAsync(Id);
            _manager.Save();
        }

        public IEnumerable<Survey> GetAllSurveys(bool trackChanges)
        {
            return _manager.Survey.GetAllSurveys(trackChanges);
        }

        public Survey GetSurveyById(bool trackChanges, int Id)
        {
            return _manager.Survey.GetSurveyById(trackChanges, Id);
        }

		public int LatestSurveyId()
		{
            return _manager.Survey.GetAllSurveys(false)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => x.Id)
                .FirstOrDefault();
		}

		public void UpdateSurvey(Survey survey)
        {
            var entity = _manager.Survey.GetSurveyById(false, survey.Id);
            entity.Id = survey.Id;
            entity.Questions = survey.Questions;
            entity.Title = survey.Title;
            entity.LastEditDate = DateTime.Now;
            _manager.Save();
        }

        public void UpdateSurveyEditTime(int Id, string userName)
        {
            var entity = _manager.Survey.GetSurveyById(false,Id);
            entity.LastEditDate = DateTime.Now;
            entity.LastEditUser = userName;
            _manager.Save();
        }
    }
}
