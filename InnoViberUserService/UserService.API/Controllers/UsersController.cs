using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.UserViewModels;
using UserService.BLL.Interfaces;
using UserService.BLL.Models;

namespace UserService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
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
    public async Task<IEnumerable<UserViewModel>> GetAll(CancellationToken ct)
    {
        var models = await _service.GetAll(ct);
        return _mapper.Map<List<UserViewModel>>(models);
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public async Task<UserViewModel> GetById(Guid id, CancellationToken ct)
    {
        var model = await _service.GetById(id, ct);
        return _mapper.Map<UserViewModel>(model);
    }

    // GET api/<UserController>/auth/5
    [HttpGet("auth/{authId}")]
    public async Task<UserViewModel> GetByAuthId(string authId, CancellationToken ct)
    {
        var model = await _service.GetByAuthId(authId, ct);
        return _mapper.Map<UserViewModel>(model);
    }

    // POST api/<UserController>/auth
    [HttpPost("auth")]
    public async Task<UserViewModel> GetOrCreate([FromBody] UserShortViewModel vm, CancellationToken ct)
    {
        var model = _mapper.Map<UserModel>(vm);
        var result = await _service.GetOrCreate(model, ct);
        return _mapper.Map<UserViewModel>(result);
    }

    // POST api/<UserController>
    [HttpPost]
    public async Task<UserViewModel> Create([FromBody] UserShortViewModel vm, CancellationToken ct)
    {
        var model = _mapper.Map<UserModel>(vm);
        var result = await _service.Create(model, ct);
        return _mapper.Map<UserViewModel>(result);
    }

    // PUT api/<UserController>/5
    [HttpPut("{id}")]
    public async Task<UserViewModel> Update(Guid id, [FromBody] UserShortViewModel vm, CancellationToken ct)
    {
        var model = _mapper.Map<UserModel>(vm);
        var result = await _service.Update(id, model, ct);
        return _mapper.Map<UserViewModel>(result);
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    public Task Delete(Guid id, CancellationToken ct)
    {
        return _service.Delete(id, ct);
    }
}
