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
    public async Task<PaginatedModel<ChatRoleViewModel>> GetByChatId(Guid chatId, CancellationToken ct, int? page, string? userId)
    {
        if(page is null)
        {
            var models = await _service.GetByChatId(chatId, ct);
            if (userId is not null)
            {
                var viewModels = _mapper.Map<List<ChatRoleViewModel>>(models);
                return new PaginatedModel<ChatRoleViewModel>
                {
                    Total = 1,
                    Items = [viewModels.Find(x => x.UserId == userId)],
                    Limit = 1,
                    Page = 1,
                    Count = 1,
                };
            }
            return new PaginatedModel<ChatRoleViewModel> {Items = _mapper.Map<List<ChatRoleViewModel>>(models)};
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

    // GET api/<ChatRoleController>/time/5
    [HttpGet("time")]
    public async Task<ChatRoleViewModel> ChangeDate(Guid chatId, string userId, DateTime lastActivity, CancellationToken ct)
    {
        var model = (await _service.GetByChatId(chatId, ct)).Find(x => x.UserId == userId);
        model!.LastActivity = lastActivity;
        model = await _service.Update(model.Id, model, ct);
        return _mapper.Map<ChatRoleViewModel>(model);
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
