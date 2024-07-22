using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.ChatRole;
using InnoViber.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using InnoViber.Domain;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatService _service;
    private readonly IMapper _mapper;
    private readonly ChatRoleController _roleController;

    public ChatController(IChatService service, IMapper mapper, ChatRoleController roleController)
    {
        _service = service;
        _mapper = mapper;
        _roleController = roleController;
    }

    // GET: api/<ChatController>
    [HttpGet]
    public async Task<IEnumerable<ChatViewModel>> Get(CancellationToken ct)
    {
        var models = await _service.GetAll(ct);
        return _mapper.Map<List<ChatViewModel>>(models);
    }

    // GET api/<ChatController>/5
    [HttpGet("{id}")]
    public async Task<ChatViewModel> GetById(Guid id, CancellationToken ct)
    {
        var model = await _service.GetById(id, ct);
        return _mapper.Map<ChatViewModel>(model);
    }

    // GET api/<ChatController>/user/5
    [HttpGet("user/{userId}")]
    public async Task<PaginatedModel<ChatViewModel>> GetByUserId(string userId, CancellationToken ct, int? page)
    {
        if(page is null){
            var model = await _service.GetByUserId(userId, ct);
            return new PaginatedModel<ChatViewModel> { Items = _mapper.Map<List<ChatViewModel>>(model) };
        }
        else
        {
            PaginatedModel<ChatModel> models = await _service.PaginateByUserId(userId, Constants.LIMIT, page ?? 1, ct);
            var viewModels = _mapper.Map<List<ChatViewModel>>(models.Items);
            return new PaginatedModel<ChatViewModel>
            {
                Items = viewModels,
                Limit = Constants.LIMIT,
                Page = page,
                Count = models.Count,
            };
        }
    }

    // POST api/<ChatController>
    [HttpPost]
    public async Task<ChatViewModel> Create([FromBody] CreateChatViewModel chat, CancellationToken ct)
    {
        var model = _mapper.Map<ChatModel>(chat);
        var chatModel = await _service.Create(model, ct);
        await _roleController.Create(new ChatRoleShortViewModel { Role = UserRoles.Owner, ChatId = chatModel.Id, UserId = chat.UserId}, ct);
        return _mapper.Map<ChatViewModel>(chatModel);
    }

    // PUT api/<ChatController>/5
    [HttpPut("{id}")]
    public async Task<ChatViewModel> Update(Guid id, [FromBody] ChatShortViewModel chat, CancellationToken ct)
    {
        var model = _mapper.Map<ChatModel>(chat);
        var chatModel = await _service.Update(id, model, ct);
        return _mapper.Map<ChatViewModel>(chatModel);
    }

    // DELETE api/<ChatController>/5
    [HttpDelete("{id}")]
    public Task Delete(Guid id, CancellationToken ct)
    {
        return _service.Delete(id, ct);
    }
}
