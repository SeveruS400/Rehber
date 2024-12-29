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
    public class ReferanceManager : IReferanceService
    {
        private readonly IRepositoryManager _manager;

        public ReferanceManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task AddReferance(Referance referance)
        {
            _manager.Referance.AddReferance(referance);
        }

        public IEnumerable<Referance> GetAllReferances(bool trackChanges)
        {
            return _manager.Referance.GetAllReferances(trackChanges);
        }
    }
}
