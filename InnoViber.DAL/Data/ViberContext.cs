using InnoViber.DAL.Models;
using InnoViber.Domain.Providers;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Data;

public class ViberContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public ViberContext(DbContextOptions<ViberContext> options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
        if (Database.IsRelational())
        {
            Database.Migrate();
        }
    }

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

    public DbSet<ChatEntity> Chats { get; set; }


    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var utcNow = _dateTimeProvider.GetDate();
        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            var track = entry.Entity as BaseEntity;
            switch (entry.State)
            {
                case EntityState.Modified:
                    Entry(track)!.Property(x => x.CreatedAt).IsModified = false;
                    track!.UpdatedAt = utcNow;
                    break;

                case EntityState.Added:
                    track!.CreatedAt = utcNow;
                    track!.UpdatedAt = utcNow;
                    break;
            }
        }

        return base.SaveChanges();
    }
}
