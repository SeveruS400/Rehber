using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class SurveyQuestion : Base
	{
		public int SurveyId { get; set; }
		public string Text { get; set; }
		public string AnswerType { get; set; } 
		public Survey Survey { get; set; }
		public ICollection<SurveyResponse> Responses { get; set; }
	}
}
