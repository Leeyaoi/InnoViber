﻿using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.ChatRole;
using InnoViber.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace InnoViber.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    [Authorize]
    public async Task<IEnumerable<ChatViewModel>> Get()
    {
        var models = await _service.GetAll(default);
        return _mapper.Map<List<ChatViewModel>>(models);
    }

    // GET api/<ChatController>/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ChatViewModel> GetById(Guid id)
    {
        var model = await _service.GetById(id, default);
        return _mapper.Map<ChatViewModel>(model);
    }

    // GET api/<ChatController>/user/5
    [HttpGet("user/{userId}")]
    [Authorize]
    public async Task<IEnumerable<ChatViewModel>> GetByUserId(Guid userId)
    {
        var model = await _service.GetByUserId(userId, default);
        return _mapper.Map<IEnumerable<ChatViewModel>>(model);
    }

    // POST api/<ChatController>
    [HttpPost]
    [Authorize]
    public async Task<ChatViewModel> Create([FromBody] CreateChatViewModel chat)
    {
        var model = _mapper.Map<ChatModel>(chat);
        var chatModel = await _service.Create(model, default);
        await _roleController.Create(new ChatRoleShortViewModel { Role = UserRoles.Owner, ChatId = chatModel.Id, UserId = chat.UserId});
        return _mapper.Map<ChatViewModel>(chatModel);
    }

    // PUT api/<ChatController>/5
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ChatViewModel> Update(Guid id, [FromBody] ChatShortViewModel chat)
    {
        var model = _mapper.Map<ChatModel>(chat);
        var chatModel = await _service.Update(id, model, default);
        return _mapper.Map<ChatViewModel>(chatModel);
    }

    // DELETE api/<ChatController>/5
    [HttpDelete("{id}")]
    [Authorize]
    public Task Delete(Guid id)
    {
        return _service.Delete(id, default);
    }
}
