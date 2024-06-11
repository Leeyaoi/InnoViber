using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public IEnumerable<ChatViewModel> Get()
    {
        var models = _service.GetAll(default);
        return _mapper.Map<List<ChatViewModel>>(models);
    }

    // GET api/<ChatController>/5
    [HttpGet("{id}")]
    public ChatViewModel Get(Guid id)
    {
        var model = _service.GetById(id, default);
        return _mapper.Map<ChatViewModel>(model);
    }

    // POST api/<ChatController>
    [HttpPost]
    public void Post([FromBody] ChatViewModel chat)
    {
        var model = _mapper.Map<ChatModel>(chat);
        _service.Create(model, default);
    }

    // PUT api/<ChatController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] ChatViewModel chat)
    {
        var model = _mapper.Map<ChatModel>(chat);
        _service.Update(model, default);
    }

    // DELETE api/<ChatController>/5
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        var model = _mapper.Map<ChatModel>(_service.GetById(id, default));
        _service.Delete(model, default);
    }
}
