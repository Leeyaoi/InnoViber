using InnoViber.DAL.Entities;
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

    public DbSet<ChatRoleEntity> ChatRoles { get; set; }


    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var utcNow = _dateTimeProvider.GetDate();
        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            var entity = entry.Entity as BaseEntity;

            if (entity == null) { continue; }

            switch (entry.State)
            {
                case EntityState.Modified:
                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    entity.UpdatedAt = utcNow;
                    break;

                case EntityState.Added:
                    entity.CreatedAt = utcNow;
                    entity.UpdatedAt = utcNow;
                    break;
            }
        }

        return base.SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
