using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Survey: Base
	{
		public string Title { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime LastEditDate { get; set; } = DateTime.Now;
		public ICollection<SurveyQuestion> Questions { get; set; }
		public string CreatedUser { get; set; }
		public string LastEditUser { get; set; }
	}
}
