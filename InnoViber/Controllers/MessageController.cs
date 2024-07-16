using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using InnoViber.API.ViewModels.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController : ControllerBase
{
    private readonly IMessageService _service;
    private readonly IMapper _mapper;

    public MessageController(IMessageService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/<MessageController>
    [HttpGet]
    public async Task<IEnumerable<MessageViewModel>> Get(CancellationToken ct)
    {
        var models = await _service.GetAll(ct);
        return _mapper.Map<List<MessageViewModel>>(models);
    }

    // GET api/<MessageController>/5
    [HttpGet("{id}")]
    public async Task<MessageViewModel> GetById(Guid id, CancellationToken ct)
    {
        var model = await _service.GetById(id, ct);
        return _mapper.Map<MessageViewModel>(model);
    }

    // GET api/<MessageController>/chat/5
    [HttpGet("Chat/{chatId}")]
    public async Task<IEnumerable<MessageViewModel>> GetByChatId(Guid chatId, CancellationToken ct)
    {
        var model = await _service.GetByChatId(chatId, ct);
        return _mapper.Map<IEnumerable<MessageViewModel>>(model);
    }

    // POST api/<MessageController>
    [HttpPost]
    public async Task<MessageViewModel> Create([FromBody] MessageShortViewModel message, CancellationToken ct)
    {
        var model = _mapper.Map<MessageModel>(message);
        var messageModel = await _service.Create(model, ct);
        return _mapper.Map<MessageViewModel>(messageModel);
    }

    // PUT api/<MessageController>/5
    [HttpPut("{id}")]
    public async Task<MessageViewModel> Update(Guid id, [FromBody] MessageShortViewModel message, CancellationToken ct)
    {
        var model = _mapper.Map<MessageModel>(message);
        var messageModel = await _service.Update(id, model, ct);
        return _mapper.Map<MessageViewModel>(messageModel);
    }

    // PUT api/<MessageController>/status/5
    [HttpPut("status/{id}")]
    public async Task<MessageViewModel> UpdateStatus(Guid id, [FromBody] MessageChangeStatusViewModel message, CancellationToken ct)
    {
        var model = _mapper.Map<MessageModel>(_service.GetById(id, ct));
        var messageModel = await _service.UpdateStatus(message.Status, model, ct);
        return _mapper.Map<MessageViewModel>(messageModel);
    }

    // DELETE api/<MessageController>/5
    [HttpDelete("{id}")]
    public Task Delete(Guid id, CancellationToken ct)
    {
        return _service.Delete(id, ct);
    }
}
