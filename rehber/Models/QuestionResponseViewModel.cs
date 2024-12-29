namespace rehber.Models
{
    public class QuestionResponseViewModel
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string AnswerType { get; set; } // Örneğin: TextBox, RadioButton
        public string Response { get; set; } // Kullanıcı yanıtı
    }
}
