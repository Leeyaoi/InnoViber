using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Data;

public class ViberContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<Chat> Chats { get; set; }

    public ViberContext(DbContextOptions<ViberContext> options) : base(options) { }
}
