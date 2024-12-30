using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRequestSuggestionsRepository
    {
        void CreateRequestSuggestion(RequestSuggestions requestSuggestion);
        void EditRequestSuggestion(RequestSuggestions requestSuggestion);
        IEnumerable<RequestSuggestions> GetAllRequestSuggestions();
        IEnumerable<RequestSuggestions> GetAllCompletedRequestSuggestions();
        IEnumerable<RequestSuggestions> GetUncompletedRequestSuggestions();
        IEnumerable<RequestSuggestions> GetAllRequestSuggestionsByProductId(int productId);
        RequestSuggestions GetRequestSuggestionBySuggestionId(int suggestionId);
    }
}
