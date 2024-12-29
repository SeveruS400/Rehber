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
    public class SurveyRepository : RepositoryBase<Survey>, ISurveyRepository
    {
        public SurveyRepository(DataContext context) : base(context)
        {
        }

        public async Task AddSurvey(Survey survey) => await CreateAsync(survey);


        public IEnumerable<Survey> GetAllSurveys(bool trackChanges)
        {
            var Surveys = trackChanges
                ? _context.Surveys.ToList()
                : _context.Surveys.AsNoTracking().ToList();

            return Surveys;
        }
        public Survey GetSurveyById(bool trackChanges, int Id)
        {
            var Surveys  = _context.Surveys.Include(s => s.Questions).FirstOrDefault(u => u.Id == Id);
			return Surveys;
        }
    }
}
