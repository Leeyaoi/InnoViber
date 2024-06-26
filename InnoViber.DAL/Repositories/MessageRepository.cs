﻿using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

public class MessageRepository : GenericRepository<MessageEntity>, IMessageRepository
{
    public MessageRepository(ViberContext context) : base(context)
    { }

    public override Task<MessageEntity?> GetById(Guid Id, CancellationToken ct) => _dbSet.AsNoTracking()
                                                                                         .Where(x => x.Id == Id)
                                                                                         .Include(x => x.User)
                                                                                         .Include(x => x.Chat)
                                                                                         .FirstOrDefaultAsync(ct);

    public override async Task<MessageEntity> Update(MessageEntity entity, CancellationToken ct)
    {
        var result = _dbSet.Update(entity);
        _dbSet.Entry(entity).Property(x => x.Date).IsModified = false;
        await _viberContext.SaveChangesAsync(ct);
        return result.Entity;
    }

    public override Task<List<MessageEntity>> GetAll(CancellationToken ct) => _dbSet.AsNoTracking()
                                                                                    .Include(x => x.Chat)
                                                                                    .ThenInclude(c => c.Roles)
                                                                                    .ToListAsync(ct);
}
