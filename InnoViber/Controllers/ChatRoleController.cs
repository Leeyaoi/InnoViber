using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.ChatRole;
using System.Web.Http.Cors;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[EnableCors(origins: "*", headers: "*", methods: "*")]
[ApiController]
public class ChatRoleController : ControllerBase
{
    private readonly IChatRoleService _service;
    private readonly IMapper _mapper;

    public ChatRoleController(IChatRoleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/<ChatRoleController>
    [HttpGet]
    public async Task<IEnumerable<ChatRoleViewModel>> Get()
    {
        var models = await _service.GetAll(default);
        return _mapper.Map<List<ChatRoleViewModel>>(models);
    }

    // GET api/<ChatRoleController>/5
    [HttpGet("{id}")]
    public async Task<ChatRoleViewModel> GetById(Guid id)
    {
        var model = await _service.GetById(id, default);
        return _mapper.Map<ChatRoleViewModel>(model);
    }

    // POST api/<ChatRoleController>
    [HttpPost]
    public async Task<ChatRoleViewModel> Create([FromBody] ChatRoleShortViewModel role)
    {
        var model = _mapper.Map<ChatRoleModel>(role);
        var chatRoleModel = await _service.Create(model, default);
        return _mapper.Map<ChatRoleViewModel>(chatRoleModel);
    }

    // PUT api/<ChatRoleController>/5
    [HttpPut("{id}")]
    public async Task<ChatRoleViewModel> Update(Guid id, [FromBody] ChatRoleShortViewModel role)
    {
        var model = _mapper.Map<ChatRoleModel>(role);
        var chatRoleModel = await _service.Update(id, model, default);
        return _mapper.Map<ChatRoleViewModel>(chatRoleModel);
    }

    // PUT api/<ChatRoleController>/role/5
    [HttpPut("role/{id}")]
    public async Task<ChatRoleViewModel> UpdateRole(Guid id, [FromBody] ChatRoleShortViewModel role)
    {
        var model = _mapper.Map<ChatRoleModel>(_service.GetById(id, default));
        var chatRoleModel = await _service.UpdateRole(role.Role, model, default);
        return _mapper.Map<ChatRoleViewModel>(chatRoleModel);
    }

    // DELETE api/<ChatRoleController>/5
    [HttpDelete("{id}")]
    public Task Delete(Guid id)
    {
        return _service.Delete(id, default);
    }
}
