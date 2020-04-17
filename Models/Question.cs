namespace backend.Models
{
    public class Question
    {
        public Question(string text)
        {
            Text = text;
        }

        public Question()
        {
            
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
    }
}