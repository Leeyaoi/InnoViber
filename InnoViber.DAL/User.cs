using System.Diagnostics.CodeAnalysis;

namespace InnoViber.DAL
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Message> Messages { get; set; } = new();
        public List<Chat> Chats { get; set; } = new();
    }
}
