using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.API.UserViewModels;
using UserService.BLL.Interfaces;
using UserService.BLL.Models;

namespace UserService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;

    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _service = userService;
    }

    // GET: api/<UserController>
    [HttpGet]
    public async Task<IEnumerable<UserViewModel>> GetAll()
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

    // GET api/<UserController>/auth/5
    [HttpGet("auth/{authId}")]
    public async Task<UserViewModel> GetByAuthId(string authId)
    {
        var model = await _service.GetByAuthId(authId, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // POST api/<UserController>
    [HttpPost]
    public async Task<UserViewModel> Create([FromBody] UserShortViewModel vm)
    {
        var model = _mapper.Map<UserModel>(vm);
        var result = await _service.Create(model, default);
        return _mapper.Map<UserViewModel>(result);
    }

    // PUT api/<UserController>/5
    [HttpPut("{id}")]
    public async Task<UserViewModel> Update(Guid id, [FromBody] UserShortViewModel vm)
    {
        var model = _mapper.Map<UserModel>(vm);
        var result = await _service.Update(id, model, default);
        return _mapper.Map<UserViewModel>(result);
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    public Task Delete(Guid id)
    {
        return _service.Delete(id, default);
    }
}
