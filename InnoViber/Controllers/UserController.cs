using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.User;
using FluentValidation;
using InnoViber.API.Extensions;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;
    private readonly IValidator<UserShortViewModel> _validator;

    public UserController(IUserService service, IMapper mapper, IValidator<UserShortViewModel> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public async Task<IEnumerable<UserViewModel>> Get()
    {
        var models = await _service.GetAll(default);
        return _mapper.Map<List<UserViewModel>>(models);
    }

    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
    public async Task<UserViewModel> GetById(Guid id)
    {
        var model = await _service.GetById(id, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // POST api/<ValuesController>
    [HttpPost]
    public async Task<UserViewModel> Create([FromBody] UserShortViewModel user)
    {
        var result = await _validator.ValidateAsync(user);
        if (!result.IsValid)
        {
            result.GenerateValidationExeption();
        }
        var model = _mapper.Map<UserModel>(user);
        await _service.Create(model, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public async Task<UserViewModel> Update(Guid id, [FromBody] UserShortViewModel user)
    {
        var result = await _validator.ValidateAsync(user);
        if (!result.IsValid)
        {
            result.GenerateValidationExeption();
        }
        var model = _mapper.Map<UserModel>(user);
        model.Id = id;
        await _service.Update(model, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public async Task<UserViewModel> Delete(Guid id)
    {
        var model = _mapper.Map<UserModel>(_service.GetById(id, default));
        await _service.Delete(model, default);
        return _mapper.Map<UserViewModel>(model);
    }
}
