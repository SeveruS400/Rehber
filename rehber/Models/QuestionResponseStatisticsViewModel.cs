namespace rehber.Models
{
    public class QuestionResponseStatisticsViewModel
    {
        public string QuestionText { get; set; }
        public int YesCount { get; set; }
        public int NoCount { get; set; }
        public double YesPercentage { get; set; }
        public double NoPercentage { get; set; }
    }
}
