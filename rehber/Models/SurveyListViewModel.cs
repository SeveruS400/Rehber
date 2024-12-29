using Entities.Models;

namespace rehber.Models
{
	public class SurveyListViewModel
	{
		public Survey Survey { get; set; } // Tek bir Survey nesnesi

		public IEnumerable<SurveyQuestion> SurveyQuestion { get; set; } = Enumerable.Empty<SurveyQuestion>();

		public IEnumerable<SurveyResponse> SurveyResponse { get; set; } = Enumerable.Empty<SurveyResponse>();
	}
}
