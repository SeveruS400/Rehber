using Entities.Models;
using Repositories.Contracts;
using Repositories.Implementations;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class SurveyResponseManager : ISurveyResponseService
    {
        private readonly IRepositoryManager _manager;

        public SurveyResponseManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void CreateResponse(SurveyResponse response)
        {
            _manager.SurveyResponse.CreateResponse(response);
            _manager.Save();
        }

        public IEnumerable<SurveyResponse> GetAllResponsesBySurveyID(int surveyId)
        {
            return _manager.SurveyResponse.GetAllResponsesBySurveyID(surveyId);
        }

        public IEnumerable<SurveyResponse> GetAllYesNoResponsesBySurveyID(int surveyId)
        {
            return _manager.SurveyResponse.GetAllYesNoResponsesBySurveyID(surveyId) ;
        }

        public IEnumerable<SurveyResponse> GetResponsesByUserId(int userId)
        {
            return _manager.SurveyResponse.GetResponsesByUserId(userId);
        }

        public IEnumerable<SurveyResponse> GetResponsesWithSurveyIDByUserId(int userId, int surveyId)
        {
            return _manager.SurveyResponse.GetResponsesWithSurveyIDByUserId(userId, surveyId);
        }

        public int LastSolvedSurveyId(int userId)
        {
            return _manager.SurveyResponse.GetLastSolvedSurveyId(userId);
        }
    }
}
