using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class SurveyResponse: Base
	{
		public int QuestionId { get; set; }
		public int UserId { get; set; }
		public string ResponseText { get; set; }
		public SurveyQuestion Question { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;
    }
}
