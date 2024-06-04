namespace InnoViber.DAL
{
    public class Chat
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        List<User> Users { get; set; } = new();
        List<Message> Messages { get; set;} = new();
    }
}
