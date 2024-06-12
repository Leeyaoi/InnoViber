using AutoMapper;
using InnoViber.API.ViewModels;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using InnoViber.API.ViewModels.Message;
using InnoViber.API.Extensions;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _service;
    private readonly IMapper _mapper;
    private readonly IValidator<MessageShortViewModel> _validator;
    private readonly IValidator<MessageChangeStatusViewModel> _statusValidator;

    public MessageController(IMessageService service, IMapper mapper, 
        IValidator<MessageShortViewModel> validator, IValidator<MessageChangeStatusViewModel> statusValidator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
        _statusValidator = statusValidator;
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
        var result = await _validator.ValidateAsync(message);
        if (!result.IsValid)
        {
            result.GenerateValidationExeption();
        }
        var model = _mapper.Map<MessageModel>(message);
        await _service.Create(model, default);
        return _mapper.Map<MessageViewModel>(model);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public async Task<MessageViewModel> Update(Guid id, [FromBody] MessageShortViewModel message)
    {
        var result = await _validator.ValidateAsync(message);
        if (!result.IsValid)
        {
            result.GenerateValidationExeption();
        }
        var model = _mapper.Map<MessageModel>(message);
        model.Id = id;
        await _service.Update(model, default);
        return _mapper.Map<MessageViewModel>(model);
    }

    // PUT api/<ValuesController>/status/5
    [HttpPut("status/{id}")]
    public async Task<MessageViewModel> UpdateStatus(Guid id, [FromBody] MessageChangeStatusViewModel message)
    {
        var result = await _statusValidator.ValidateAsync(message);
        if (!result.IsValid)
        {
            result.GenerateValidationExeption();
        }
        var model = _mapper.Map<MessageModel>(_service.GetById(id, default));
        model.Status = message.Status;
        await _service.Update(model, default);
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
