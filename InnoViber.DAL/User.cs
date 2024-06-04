namespace InnoViber.DAL
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public List<Message> Messages { get; set; } = new();
        public List<Chat> Chats { get; set; } = new();
    }
}
