using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ISurveyQuestionRepository : IRepositoryBase<SurveyQuestion>
    {
        IEnumerable<SurveyQuestion> GetAllSurveyQuestionBySurveyId(bool trackChanges, int Id);
        int GetLastYesNoSurveyID();
    }
}
