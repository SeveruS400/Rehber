using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class EducationStatus :Base
	{
		public string EducationStatusName { get; set; }
		public ICollection<Products> Products { get; set; }
	}
}
