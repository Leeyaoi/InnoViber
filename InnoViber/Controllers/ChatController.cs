using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.Chat;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _service;
    private readonly IMapper _mapper;

    public ChatController(IChatService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/<ChatController>
    [HttpGet]
    public async Task<IEnumerable<ChatViewModel>> Get()
    {
        var models = await _service.GetAll(default);
        return _mapper.Map<List<ChatViewModel>>(models);
    }

    // GET api/<ChatController>/5
    [HttpGet("{id}")]
    public async Task<ChatViewModel> GetById(Guid id)
    {
        var model = await _service.GetById(id, default);
        return _mapper.Map<ChatViewModel>(model);
    }

    // POST api/<ChatController>
    [HttpPost]
    public async Task<ChatViewModel> Create([FromBody] ChatShortViewModel chat)
    {
        var model = _mapper.Map<ChatModel>(chat);
        await _service.Create(model, default);
        return _mapper.Map<ChatViewModel>(model);
    }

    // PUT api/<ChatController>/5
    [HttpPut("{id}")]
    public async Task<ChatViewModel> Update(Guid id, [FromBody] ChatShortViewModel chat)
    {
        var model = _mapper.Map<ChatModel>(chat);
        await _service.Update(id, model, default);
        return _mapper.Map<ChatViewModel>(model);
    }

    // PUT api/<ChatController>/5
    [HttpPut("{id}/{UserId}")]
    public async Task<ChatViewModel> UpdateUsersList(Guid id, Guid UserId)
    {
        var result = await _service.UpdateUsersList(id, UserId, default);
        return _mapper.Map<ChatViewModel>(result);
    }

    // DELETE api/<ChatController>/5
    [HttpDelete("{id}")]
    public Task Delete(Guid id)
    {
        return _service.Delete(id, default);
    }
}
