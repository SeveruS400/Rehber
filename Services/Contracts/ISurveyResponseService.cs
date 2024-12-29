﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ISurveyResponseService
    {
        void CreateResponse(SurveyResponse response);
        IEnumerable<SurveyResponse> GetResponsesByUserId(int userId);
        int LastSolvedSurveyId(int userId);
        IEnumerable<SurveyResponse> GetResponsesWithSurveyIDByUserId(int userId, int surveyId);
        IEnumerable<SurveyResponse> GetAllResponsesBySurveyID(int surveyId);
        IEnumerable<SurveyResponse> GetAllYesNoResponsesBySurveyID(int surveyId);

    }
}