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
	public class EducationStatusRepository : RepositoryBase<EducationStatus>, IEducationStatusRepository
	{
		public EducationStatusRepository(DataContext context) : base(context)
		{
		}

		public IEnumerable<EducationStatus> GetAllEducationStatus(bool trackChanges)
		{
			var EducationStatus = trackChanges
				? _context.Educations.ToList()
				: _context.Educations.AsNoTracking().ToList();

			return EducationStatus;
		}
	}
}
