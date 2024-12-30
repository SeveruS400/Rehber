using Entities.Models;

namespace rehber.Models
{
    public class ProductViewModel
    {
        public Products Product { get; set; }

        public IEnumerable<Categories> Categories { get; set; } = Enumerable.Empty<Categories>();

        public IEnumerable<EducationStatus> EducationStatus { get; set; } = Enumerable.Empty<EducationStatus>();

        public IEnumerable<Referance> Referance { get; set; } = Enumerable.Empty<Referance>();
        public IEnumerable<Survey> Survey { get; set; } = Enumerable.Empty<Survey>();
        public HashSet<int> AnsweredSurveyIds { get; set; }
		public LastSurveyViewModel LastAnsweredSurvey { get; set; }
        public IEnumerable<RequestSuggestions> RequestSuggestions { get; set; }


    }
}
