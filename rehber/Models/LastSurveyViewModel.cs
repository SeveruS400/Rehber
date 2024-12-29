namespace rehber.Models
{
	public class LastSurveyViewModel
	{
		public int SurveyId { get; set; }
		public string SurveyTitle { get; set; }
		public List<QuestionAnswerViewModel> QuestionsAndAnswers { get; set; }
	}
}
