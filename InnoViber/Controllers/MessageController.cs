using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _service;
    private readonly IMapper _mapper;

    public MessageController(IMessageService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public async Task<IEnumerable<MessageViewModel>> Get()
    {
        var models = await _service.GetAll(default);
        return _mapper.Map<List<MessageViewModel>>(models);
    }

    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
    public async Task<MessageViewModel> GetById(Guid id)
    {
        var model = await _service.GetById(id, default);
        return _mapper.Map<MessageViewModel>(model);
    }

    // POST api/<ValuesController>
    [HttpPost]
    public void Create([FromBody] MessageShortViewModel message)
    {
        var model = _mapper.Map<MessageModel>(message);
        _service.Create(model, default);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public void Update(Guid id, [FromBody] MessageShortViewModel message)
    {
        var model = _mapper.Map<MessageModel>(message);
        _service.Update(id, model, default);
    }

    // PUT api/<ValuesController>/status/5
    [HttpPut("status/{id}")]
    public void UpdateStatus(Guid id, [FromBody] MessageChangeStatusViewModel message)
    {
        var model = _mapper.Map<MessageModel>(_service.GetById(id, default));
        _service.UpdateStatus(message.Status, model, default);
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        var model = _mapper.Map<MessageModel>(_service.GetById(id, default));
        _service.Delete(model, default);
    }
}
