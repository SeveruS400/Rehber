using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IReferanceRepository
    {
        IEnumerable<Referance> GetAllReferances(bool trackChanges);
        Task AddReferance(Referance referance);
    }
}
