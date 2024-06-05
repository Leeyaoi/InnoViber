namespace InnoViber.DAL
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        List<User> Users { get; set; } = new();
        List<Message> Messages { get; set;} = new();
    }
}
