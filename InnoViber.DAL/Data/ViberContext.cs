using InnoViber.DAL.Models;
using InnoViber.Domain.Providers;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Data;

public class ViberContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public ViberContext(IDateTimeProvider dateTimeProvider) => _dateTimeProvider = dateTimeProvider;

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

    public DbSet<ChatEntity> Chats { get; set; }

    public ViberContext(DbContextOptions<ViberContext> options) : base(options) { }

    public override int SaveChanges()
    {
        this.ChangeTracker.DetectChanges();
        var added = ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Added)
                    .Select(t => t.Entity)
                    .ToArray();

        foreach (var entity in added)
        {
            if (entity is BaseEntity)
            {
                var track = entity as BaseEntity;
                track!.CreatedAt = _dateTimeProvider.GetDate();
                track!.UpdatedAt = _dateTimeProvider.GetDate();
            }
        }

        var modified = ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Modified)
                    .Select(t => t.Entity)
                    .ToArray();

        foreach (var entity in modified)
        {
            if (entity is BaseEntity)
            {
                var track = entity as BaseEntity;
                track!.UpdatedAt = _dateTimeProvider.GetDate();
                Entry(track).State = EntityState.Modified;
                Entry(track).Property(x => x.CreatedAt).IsModified = false;
                Entry(track).Property(x => x.Id).IsModified = false;
            }
        }
        return base.SaveChanges();
    }
}
