using InnoViber.BLL.Models;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Interfaces;

public interface IMessageService : IGenericService<MessageModel>
{
    Task UpdateStatus(MessageStatus status, MessageModel model, CancellationToken ct);
}
