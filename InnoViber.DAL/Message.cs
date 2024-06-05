using System.Diagnostics.CodeAnalysis;

namespace InnoViber.DAL
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime Date {  get; set; }
        public string Text { get; set; } = string.Empty;
        public int Status { get; set; }
        public User? User { get; set; }
        public Chat? Chat { get; set; }
    }
}
