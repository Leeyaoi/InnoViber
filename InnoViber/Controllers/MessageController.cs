using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly MessageService _service;
    private readonly IMapper _mapper;

    public MessageController(MessageService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public IEnumerable<MessageViewModel> Get()
    {
        var models = _service.GetAll(default);
        return _mapper.Map<List<MessageViewModel>>(models);
    }

    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
    public MessageViewModel Get(Guid id)
    {
        var model = _service.GetById(id, default);
        return _mapper.Map<MessageViewModel>(model);
    }

    // POST api/<ValuesController>
    [HttpPost]
    public void Post([FromBody] MessageModel message)
    {
        var model = _mapper.Map<MessageModel>(message);
        _service.Create(model, default);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] MessageModel message)
    {
        var model = _mapper.Map<MessageModel>(message);
        _service.Update(model, default);
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        var model = _mapper.Map<MessageModel>(_service.GetById(id, default));
        _service.Delete(model, default);
    }
}
