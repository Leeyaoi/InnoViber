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
}
