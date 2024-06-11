﻿using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using InnoViber.BLL.Helpers;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Moq;
using Shouldly;

namespace InnoViber.Test.Service;

public class ChatServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IChatRepository> _repoMock;
    private readonly ChatService _service;

    public ChatServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
            cfg.AddExpressionMapping();
        });

        _mapper = config.CreateMapper();

        _repoMock = new Mock<IChatRepository>();

        _service = new ChatService(_mapper, _repoMock.Object);
    }

    [Theory, AutoData]
    public async Task GetAllChatsTest_HasData_ReturnsChats([NoAutoProperties] List<ChatEntity> chats)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetAll(default)).ReturnsAsync(chats);

        //Act

        var result = await _service.GetAll(default);

        //Assert

        var entity = _mapper.Map<List<ChatEntity>>(result);
        entity.ShouldBeEquivalentTo(chats);
    }

    [Theory, AutoData]
    public async Task GetByIdTest_HasData_ReturnsChat([NoAutoProperties] ChatEntity chat)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetById(Guid.Empty, default)).ReturnsAsync(chat);

        //Act

        var result = await _service.GetById(Guid.Empty, default);

        //Assert

        var entity = _mapper.Map<ChatEntity>(result);
        entity.ShouldBeEquivalentTo(chat);
    }

    [Theory, AutoData]
    public async Task GetByPredicateTest_HasData_ReturnsChat([NoAutoProperties] ChatEntity chat)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.Name == "Test1", default)).ReturnsAsync(chat);

        //Act

        var result = await _service.GetByPredicate(x => x.Name == "Test1", default);

        //Assert

        var entity = _mapper.Map<ChatEntity>(result);
        entity.ShouldBeEquivalentTo(chat);
    }
}