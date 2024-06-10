using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;

namespace InnoViber.Controllers;

public class ChatController
{
    private readonly ChatService _service;
    private readonly IMapper _mapper;

    public ChatController(ChatService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public Task Create(ChatViewModel chat, CancellationToken ct)
    {
        var model = _mapper.Map<ChatModel>(chat);
        return _service.Create(model, ct);
    }

    public Task Delete(ChatViewModel chat, CancellationToken ct)
    {
        var model = _mapper.Map<ChatModel>(chat);
        return _service.Delete(model, ct);
    }

    public Task Update(ChatViewModel chat, CancellationToken ct)
    {
        var model = _mapper.Map<ChatModel>(chat);
        return _service.Update(model, ct);
    }

    public async Task<List<ChatViewModel>> GetAll(CancellationToken ct)
    {
        var models = await _service.GetAll(ct);
        return _mapper.Map<List<ChatViewModel>>(models);
    }

    public async Task<ChatViewModel?> GetById(Guid id, CancellationToken ct)
    {
        var model = await _service.GetById(id, ct);
        return _mapper?.Map<ChatViewModel>(model);
    }
}
