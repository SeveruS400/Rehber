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
	public class EducationStatusManager : IEducationStatusService
	{
		private readonly IRepositoryManager _manager;

		public EducationStatusManager(IRepositoryManager manager)
		{
			_manager = manager;
		}

		public IEnumerable<EducationStatus> GetAllEducationStatus(bool trackChanges)
		{
			return _manager.EducationStatus.GetAllEducationStatus(trackChanges);
		}
	}
}
