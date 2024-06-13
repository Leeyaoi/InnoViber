using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using InnoViber.API.ViewModels.Message;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<MessageViewModel> Create([FromBody] MessageShortViewModel message)
    {
        var model = _mapper.Map<MessageModel>(message);
        await _service.Create(model, default);
        return _mapper.Map<MessageViewModel>(model);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public async Task<MessageViewModel> Update(Guid id, [FromBody] MessageShortViewModel message)
    {
        var model = _mapper.Map<MessageModel>(message);
        await _service.Update(id, model, default);
        return _mapper.Map<MessageViewModel>(model);
    }

    // PUT api/<ValuesController>/status/5
    [HttpPut("status/{id}")]
    public async Task<MessageViewModel> UpdateStatus(Guid id, [FromBody] MessageChangeStatusViewModel message)
    {
        var model = _mapper.Map<MessageModel>(_service.GetById(id, default));
        await _service.UpdateStatus(message.Status, model, default);
        return _mapper.Map<MessageViewModel>(model);
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public async Task<MessageViewModel> Delete(Guid id)
    {
        var model = _mapper.Map<MessageModel>(_service.GetById(id, default));
        await _service.Delete(model, default);
        return _mapper.Map<MessageViewModel>(model);
    }
}
