using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _service;
    private readonly IMapper _mapper;

    public UserController(UserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public IEnumerable<UserViewModel> Get()
    {
        var models = _service.GetAll(default);
        return _mapper.Map<List<UserViewModel>>(models);
    }

    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
    public UserViewModel Get(Guid id)
    {
        var model = _service.GetById(id, default);
        return _mapper.Map<UserViewModel>(model);
    }

    // POST api/<ValuesController>
    [HttpPost]
    public void Post([FromBody] UserModel user)
    {
        var model = _mapper.Map<UserModel>(user);
        _service.Create(model, default);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] UserModel user)
    {
        var model = _mapper.Map<UserModel>(user);
        _service.Update(model, default);
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        var model = _mapper.Map<UserModel>(_service.GetById(id, default));
        _service.Delete(model, default);
    }
}
