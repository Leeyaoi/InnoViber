using InnoViber.DAL.Models;
using InnoViber.Domain.Enums;
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
            var entity = entry.Entity as BaseEntity;
            switch (entry.State)
            {
                case EntityState.Modified:
                    Entry(entity!).Property(x => x.CreatedAt).IsModified = false;
                    entity!.UpdatedAt = utcNow;
                    break;

                case EntityState.Added:
                    entity!.CreatedAt = utcNow;
                    entity!.UpdatedAt = utcNow;
                    break;
            }

            if(entry.Entity is MessageEntity)
            {
                var message = entry.Entity as MessageEntity;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        Entry(message!).Property(x => x.Date).IsModified = false;
                        break;

                    case EntityState.Added:
                        message!.Date = utcNow;
                        message!.Status = MessageStatus.Send;
                        break;
                }
            }
        }

        return base.SaveChanges();
    }
}
