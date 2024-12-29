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
    public class RequestSuggestionsRepository : RepositoryBase<RequestSuggestions>, IRequestSuggestionsRepository
    {
        public RequestSuggestionsRepository(DataContext context) : base(context)
        {
        }

        public void CreateRequestSuggestion(RequestSuggestions requestSuggestion)
        {
            _context.RequestSuggestions.Add(requestSuggestion);
            _context.SaveChanges();
        }
        public void EditRequestSuggestion(RequestSuggestions requestSuggestion)
        {
            _context.RequestSuggestions.Update(requestSuggestion);
            _context.SaveChanges();
        }

        public IEnumerable<RequestSuggestions> GetAllRequestSuggestions()
        {
            return GetAll(false).Include(r =>r.Products);
        }

        public IEnumerable<RequestSuggestions> GetAllCompletedRequestSuggestions()
        {
            return GetAll(false).Where(r => r.SuggestionStatus.Equals(true)).ToList();
        }

        public IEnumerable<RequestSuggestions> GetUncompletedRequestSuggestions()
        {
            return GetAll(false).Where(r => r.SuggestionStatus.Equals(false)).ToList();
        }

    }
}
