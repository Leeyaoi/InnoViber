using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Data;

public class ViberContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

    public DbSet<ChatEntity> Chats { get; set; }

    public ViberContext(DbContextOptions<ViberContext> options) : base(options) { }
}
