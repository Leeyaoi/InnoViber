using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using InnoViber.User.DAL.Models;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortUserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public ShortUserController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/<ShortUserController>
    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<UserViewModel>> Get()
    {
        var models = await _service.GetAll(default);
        return _mapper.Map<List<UserViewModel>>(models);
    }

    // GET api/<ShortUserController>/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<UserViewModel> GetById(Guid id)
    {
        var model = await _service.GetById(id, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // GET api/<ShortUserController>/auth/5
    [HttpGet("auth/{id}")]
    [Authorize]
    public async Task<List<UserViewModel>> GetByMongoId(Guid id)
    {
        var model = await _service.GetByMongoId(id, default);
        return _mapper.Map<List<UserViewModel>>(model);
    }

    // GET api/<ShortUserController>/auth
    [HttpPost("auth")]
    [Authorize]
    public Task<ExternalUserModel> GetOrCreate(ShortExternalUserModel model)
    {
        return _service.GetOrCreate(model, default);
    }

    // POST api/<ShortUserController>
    [HttpPost]
    [Authorize]
    public async Task<UserViewModel> Create([FromBody] UserShortViewModel user)
    {
        var model = _mapper.Map<UserModel>(user);
        var userModel = await _service.Create(model, default);
        return _mapper.Map<UserViewModel>(userModel);
    }

    // DELETE api/<ShortUserController>/5
    [HttpDelete("{id}")]
    [Authorize]
    public Task Delete(Guid id)
    {
        return _service.Delete(id, default);
    }
}
