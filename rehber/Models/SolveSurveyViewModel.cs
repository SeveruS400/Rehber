using System.ComponentModel.DataAnnotations;

namespace rehber.Models
{
    public class SolveSurveyViewModel
    {
        public int SurveyId { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Cevaplanması zorunludur")]
        public List<QuestionResponseViewModel> Questions { get; set; }
    }
}
