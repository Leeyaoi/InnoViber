using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;

namespace InnoViber.Controllers;

public class MessageController
{
    private readonly MessageService _service;
    private readonly IMapper _mapper;

    public MessageController(MessageService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public Task Create(MessageViewModel message, CancellationToken ct)
    {
        var model = _mapper.Map<MessageModel>(message);
        return _service.Create(model, ct);
    }

    public Task Delete(MessageViewModel message, CancellationToken ct)
    {
        var model = _mapper.Map<MessageModel>(message);
        return _service.Delete(model, ct);
    }

    public Task Update(MessageViewModel message, CancellationToken ct)
    {
        var model = _mapper.Map<MessageModel>(message);
        return _service.Update(model, ct);
    }

    public async Task<List<MessageViewModel>> GetAll(CancellationToken ct)
    {
        var models = await _service.GetAll(ct);
        return _mapper.Map<List<MessageViewModel>>(models);
    }

    public async Task<MessageViewModel?> GetById(Guid id, CancellationToken ct)
    {
        var model = await _service.GetById(id, ct);
        return _mapper?.Map<MessageViewModel>(model);
    }
}
