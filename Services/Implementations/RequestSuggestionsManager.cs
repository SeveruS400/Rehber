﻿using AutoMapper;
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
    public class RequestSuggestionsManager : IRequestSuggestionsService
    {
        private readonly IRepositoryManager _manager;

        public RequestSuggestionsManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
        public void CreateRequestSuggestion(RequestSuggestions requestSuggestion)
        {
            _manager.RequestSuggestions.CreateRequestSuggestion(requestSuggestion);
        }

        public void EditRequestSuggestion(RequestSuggestions requestSuggestion)
        {
            _manager.RequestSuggestions.EditRequestSuggestion(requestSuggestion);
        }

        public IEnumerable<RequestSuggestions> GetAllCompletedRequestSuggestions()
        {
            return _manager.RequestSuggestions.GetAllCompletedRequestSuggestions();
        }

        public IEnumerable<RequestSuggestions> GetAllRequestSuggestions()
        {
            return _manager.RequestSuggestions.GetAllRequestSuggestions();
        }

        public IEnumerable<RequestSuggestions> GetUncompletedRequestSuggestions()
        {
            return _manager.RequestSuggestions.GetUncompletedRequestSuggestions();
        }
    }
}