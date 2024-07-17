using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

public class ChatRoleRepository : GenericRepository<ChatRoleEntity>, IChatRoleRepository
{
    public ChatRoleRepository(ViberContext context) : base(context)
    { }

    public override Task<ChatRoleEntity?> GetById(Guid Id, CancellationToken ct) => _dbSet.AsNoTracking().Where(x => x.Id == Id)
            .Include(x => x.Chat)
            .FirstOrDefaultAsync(ct);

    public override async Task<ChatRoleEntity> Update(ChatRoleEntity entity, CancellationToken ct)
    {
        var result = _dbSet.Update(entity);
        _dbSet.Entry(entity).Property(x => x.UserId).IsModified = false;
        _dbSet.Entry(entity).Property(x => x.ChatId).IsModified = false;
        await _viberContext.SaveChangesAsync(ct);
        return result.Entity;
    }
}
