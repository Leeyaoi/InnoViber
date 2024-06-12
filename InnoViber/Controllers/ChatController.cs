using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.Chat;
using FluentValidation;
using InnoViber.API.Extensions;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _service;
    private readonly IMapper _mapper;
    private readonly IValidator<ChatShortViewModel> _validator;

    public ChatController(IChatService service, IMapper mapper, IValidator<ChatShortViewModel> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
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
    public void Create([FromBody] ChatShortViewModel chat)
    {
        var result = _validator.Validate(chat);
        if (!result.IsValid)
        {
            result.GenerateValidationExeption();
        }
        var model = _mapper.Map<ChatModel>(chat);
        _service.Create(model, default);
    }

    // PUT api/<ChatController>/5
    [HttpPut("{id}")]
    public void Update(Guid id, [FromBody] ChatShortViewModel chat)
    {
        var result = _validator.Validate(chat);
        if (!result.IsValid)
        {
            result.GenerateValidationExeption();
        }
        var model = _mapper.Map<ChatModel>(chat);
        model.Id = id;
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
