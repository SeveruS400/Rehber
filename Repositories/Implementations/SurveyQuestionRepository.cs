using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class SurveyQuestionRepository : RepositoryBase<SurveyQuestion>, ISurveyQuestionRepository
    {
        public SurveyQuestionRepository(DataContext context) : base(context)
        {
        }

        public IEnumerable<SurveyQuestion> GetAllSurveyQuestionBySurveyId(bool trackChanges, int Id)
        {
            return GetAll(trackChanges)
                .Where(p => p.SurveyId==Id);
        }

        public int GetLastYesNoSurveyID()
        {
            var lastYesNoSurvey = _context.SurveyQuestions
               .Where(q => q.AnswerType == "YesNo") // YesNo tipindeki soruları filtrele
               .OrderByDescending(q => q.SurveyId) // SurveyId'ye göre en son eklenenleri sırala
               .FirstOrDefault(); // İlk (en son) soruyu al

            if (lastYesNoSurvey != null)
            {
                return lastYesNoSurvey.SurveyId; // En son anketin ID'sini döndür
            }

            return 0;

        }
    }
}
