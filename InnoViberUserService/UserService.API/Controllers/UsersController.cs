using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UserService.API.UserViewModels;
using UserService.BLL.Interfaces;
using UserService.BLL.Models;

namespace UserService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;

    public UsersController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _service = userService;
    }

    // GET: api/<UsersController>
    [HttpGet]
    public async Task<IEnumerable<UserViewModel>> GetAll()
    {
        var models = await _service.GetAll(default);
        return _mapper.Map<List<UserViewModel>>(models);
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<UserViewModel> GetById(ObjectId id)
    {
        var model = await _service.GetById(id, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<UserViewModel> Create([FromBody] UserShortViewModel vm)
    {
        var model = _mapper.Map<UserModel>(vm);
        var result = await _service.Create(model, default);
        return _mapper.Map<UserViewModel>(result);
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<UserViewModel> Update(ObjectId id, [FromBody] UserShortViewModel vm)
    {
        var model = _mapper.Map<UserModel>(vm);
        var result = await _service.Update(id, model, default);
        return _mapper.Map<UserViewModel>(result);
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public Task Delete(ObjectId id)
    {
        return _service.Delete(id, default);
    }
}
