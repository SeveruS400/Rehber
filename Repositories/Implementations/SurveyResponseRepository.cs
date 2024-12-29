using Azure;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class SurveyResponseRepository : RepositoryBase<SurveyResponse>, ISurveyResponseRepository
    {
        public SurveyResponseRepository(DataContext context) : base(context)
        {
        }
        public void CreateResponse(SurveyResponse response)
        {
            _context.SurveyResponses.Add(response);
            _context.SaveChanges();
        }


        public int GetLastSolvedSurveyId(int userId)
        {
            var lastResponse = _context.SurveyResponses
                .Where(response => response.UserId == userId) // Kullanıcıya ait cevaplar
                .OrderByDescending(response => response.EditDate) // Tarihe göre sırala
                .FirstOrDefault(); // En son cevabı al

            // Eğer cevap bulunamazsa varsayılan olarak -1 veya başka bir hata değeri dönebilirsiniz
            return lastResponse?.Question.SurveyId ?? -1;
        }

        public IEnumerable<SurveyResponse> GetResponsesByUserId(int userId)
        {
            return _context.SurveyResponses
                .Include(response => response.Question) // Question ile ilişkili SurveyQuestion nesnesini alın
                .ThenInclude(question => question.Survey) // SurveyQuestion ile ilişkili Survey nesnesini alın
                .Where(response => response.UserId == userId)
                .ToList();
        }

        public IEnumerable<SurveyResponse> GetResponsesWithSurveyIDByUserId(int userId, int surveyId)
        {
            var surveyQuestionsAndResponses = _context.SurveyResponses
                .Include(response => response.Question) // Question ile ilişkili SurveyQuestion nesnesini alın
                .ThenInclude(question => question.Survey) // SurveyQuestion ile ilişkili Survey nesnesini alın
                .Where(response => response.UserId == userId && response.Question.SurveyId == surveyId) // Hem userId hem surveyId filtreleniyor
                .ToList();
            return surveyQuestionsAndResponses;
        }

        public IEnumerable<SurveyResponse> GetAllResponsesBySurveyID(int surveyId)
        {
            return _context.SurveyResponses
                .Include(response => response.Question) // Question ile ilişkili SurveyQuestion nesnesini alın
                .ThenInclude(question => question.Survey) // SurveyQuestion ile ilişkili Survey nesnesini alın
                .Where(response => response.Question.SurveyId == surveyId) // Hem userId hem surveyId filtreleniyor
                .ToList();
        }
        public IEnumerable<SurveyResponse> GetAllYesNoResponsesBySurveyID(int surveyId)
        {
            return _context.SurveyResponses
                .Include(response => response.Question) // Question ile ilişkili SurveyQuestion nesnesini alın
                .ThenInclude(question => question.Survey) // SurveyQuestion ile ilişkili Survey nesnesini alın
                .Where(response => response.Question.SurveyId == surveyId && response.Question.AnswerType == "YesNo") // Hem userId hem surveyId filtreleniyor
                .ToList();
        }
    }
}
