namespace InnoViber.DAL
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public string Text { get; set; } = "";
        public int Status { get; set; }
        public User User { get; set; } = new();
        public Chat Chat { get; set; } = new();
    }
}
