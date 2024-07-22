using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

public class ChatRepository : GenericRepository<ChatEntity>, IChatRepository
{
    public ChatRepository(ViberContext context) : base(context)
    { }

    public override Task<ChatEntity?> GetById(Guid Id, CancellationToken ct) => _dbSet.AsNoTracking().Where(x => x.Id == Id)
            .Include(x => x.Roles)
            .Include(x => x.Messages)
            .FirstOrDefaultAsync(ct);

    public Task<List<ChatEntity>> PaginateByUserId(string UserId, int limit, int page, CancellationToken ct, out int total)
    {
        total = _dbSet.AsNoTracking().Include(x => x.Roles).Where(x => x.Roles.Any(r => r.UserId == UserId)).Count();
        return _dbSet.AsNoTracking().Include(x => x.Roles).Where(x => x.Roles.Any(r => r.UserId == UserId))
        .Skip((page - 1) * limit).Take(limit).ToListAsync();
    }
}
