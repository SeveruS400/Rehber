using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IReferanceService
    {
        IEnumerable<Referance> GetAllReferances(bool trackChanges);
        Task AddReferance(Referance referance);
        int GetReferanceId(string referanceName);
    }
}
