using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.User;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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
        var model = _mapper.Map<UserModel>(user);
        await _service.Create(model, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public async Task<UserViewModel> Update(Guid id, [FromBody] UserShortViewModel user)
    {
        var model = _mapper.Map<UserModel>(user);
        await _service.Update(id, model, default);
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
