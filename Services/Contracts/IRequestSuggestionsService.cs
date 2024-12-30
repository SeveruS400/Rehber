using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRequestSuggestionsService
    {
        void CreateRequestSuggestion(RequestSuggestions requestSuggestion);
        void EditRequestSuggestion(RequestSuggestions requestSuggestion);
        IEnumerable<RequestSuggestions> GetAllRequestSuggestions();
        IEnumerable<RequestSuggestions> GetAllCompletedRequestSuggestions();
        IEnumerable<RequestSuggestions> GetUncompletedRequestSuggestions();
        IEnumerable<RequestSuggestions> GetAllRequestSuggestionsByProductId(int productId);
        RequestSuggestions GetRequestSuggestionBySuggestionId(int suggestionId);
        void UpdateRequestSuggestions(int suggestionId, string reportText, string ReguestTerminatedId);
    }
}
