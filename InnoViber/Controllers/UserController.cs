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

    // GET: api/<UserController>
    [HttpGet]
    public async Task<IEnumerable<UserViewModel>> Get()
    {
        var models = await _service.GetAll(default);
        return _mapper.Map<List<UserViewModel>>(models);
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public async Task<UserViewModel> GetById(Guid id)
    {
        var model = await _service.GetById(id, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // POST api/<UserController>
    [HttpPost]
    public async Task<UserViewModel> Create([FromBody] UserShortViewModel user)
    {
        var model = _mapper.Map<UserModel>(user);
        await _service.Create(model, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // PUT api/<UserController>/5
    [HttpPut("{id}")]
    public async Task<UserViewModel> Update(Guid id, [FromBody] UserShortViewModel user)
    {
        var model = _mapper.Map<UserModel>(user);
        await _service.Update(id, model, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    public Task Delete(Guid id)
    {
        return _service.Delete(id, default);
    }
}
