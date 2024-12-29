﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class ReferanceRepository : RepositoryBase<Referance>, IReferanceRepository
    {
        public ReferanceRepository(DataContext context) : base(context)
        {
        }

        public async Task AddReferance(Referance referance)
        {
            _context.Referances.Add(referance);
            _context.SaveChanges();
        }

        public IEnumerable<Referance> GetAllReferances(bool trackChanges)
        {
            var Referance = trackChanges
                ? _context.Referances.ToList()
                : _context.Referances.AsNoTracking().ToList();

            return Referance;
        }
    }
}
