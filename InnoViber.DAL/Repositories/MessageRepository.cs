using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

public class MessageRepository : GenericRepository<MessageEntity>, IMessageRepository
{
    public MessageRepository(ViberContext context) : base(context)
    { }

    public override Task<MessageEntity?> GetById(Guid Id, CancellationToken ct) => _dbSet.Where(x => x.Id == Id)
            .Include(x => x.User)
            .Include(x => x.Chat)
            .FirstOrDefaultAsync(ct);
}
