using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.ChatRole;
using Microsoft.AspNetCore.Authorization;
using InnoViber.Domain;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
    public async Task<IEnumerable<ChatRoleViewModel>> Get(CancellationToken ct)
    {
        var models = await _service.GetAll(ct);
        return _mapper.Map<List<ChatRoleViewModel>>(models);
    }

    // GET api/<ChatRoleController>/5
    [HttpGet("{id}")]
    public async Task<ChatRoleViewModel> GetById(Guid id, CancellationToken ct)
    {
        var model = await _service.GetById(id, ct);
        return _mapper.Map<ChatRoleViewModel>(model);
    }

    // GET api/<ChatRoleController>/chat/5
    [HttpGet("chat/{chatId}")]
    public async Task<PaginatedModel<ChatRoleViewModel>> GetByChatId(Guid chatId, CancellationToken ct, int? page)
    {
        if(page is null)
        {
            var model = await _service.GetByChatId(chatId, ct);
            return new PaginatedModel<ChatRoleViewModel> {Items = _mapper.Map<List<ChatRoleViewModel>>(model)};
        }
        else
        {
            PaginatedModel<ChatRoleModel> models = await _service.PaginateByChatId(chatId, Constants.LIMIT, page ?? 1, ct);
            var viewModels = _mapper.Map<List<ChatRoleViewModel>>(models.Items);
            return new PaginatedModel<ChatRoleViewModel>
            {
                Total = models.Total,
                Items = viewModels,
                Limit = Constants.LIMIT,
                Page = page,
                Count = models.Count,
            };
        }
    }

    // POST api/<ChatRoleController>
    [HttpPost]
    public async Task<ChatRoleViewModel> Create([FromBody] ChatRoleShortViewModel role, CancellationToken ct)
    {
        var model = _mapper.Map<ChatRoleModel>(role);
        var chatRoleModel = await _service.Create(model, ct);
        return _mapper.Map<ChatRoleViewModel>(chatRoleModel);
    }

    // PUT api/<ChatRoleController>/5
    [HttpPut("{id}")]
    public async Task<ChatRoleViewModel> Update(Guid id, [FromBody] ChatRoleShortViewModel role, CancellationToken ct)
    {
        var model = _mapper.Map<ChatRoleModel>(role);
        var chatRoleModel = await _service.Update(id, model, ct);
        return _mapper.Map<ChatRoleViewModel>(chatRoleModel);
    }

    // PUT api/<ChatRoleController>/role/5
    [HttpPut("role/{id}")]
    public async Task<ChatRoleViewModel> UpdateRole(Guid id, [FromBody] ChatRoleShortViewModel role, CancellationToken ct)
    {
        var model = _mapper.Map<ChatRoleModel>(_service.GetById(id, ct));
        var chatRoleModel = await _service.UpdateRole(role.Role, model, ct);
        return _mapper.Map<ChatRoleViewModel>(chatRoleModel);
    }

    // DELETE api/<ChatRoleController>/5
    [HttpDelete("{id}")]
    public Task Delete(Guid id, CancellationToken ct)
    {
        return _service.Delete(id, ct);
    }
}
