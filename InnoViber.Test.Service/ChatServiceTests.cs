using AutoFixture.Xunit2;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using InnoViber.BLL.Helpers;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
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
            cfg.AddProfile(new BllLayerMapperProfile());
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
    public async Task GetByPredicateTest_HasData_ReturnsChat([NoAutoProperties] List<ChatEntity> chat)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.Name == "Test1", default)).ReturnsAsync(chat);

        //Act

        var result = await _service.GetByPredicate(x => x.Name == "Test1", default);

        //Assert

        var entity = _mapper.Map<List<ChatEntity>>(result);
        entity.ShouldBeEquivalentTo(chat);
    }

    [Theory, AutoData]
    public async Task CreateTest_HasData_ReturnsChatModel([NoAutoProperties] ChatModel model)
    {
        //Arrange
        var entity = _mapper.Map<ChatEntity>(model);
        _repoMock.Setup(repo => repo.Create(It.IsAny<ChatEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.Create(model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }

    [Theory, AutoData]
    public async Task UpdateTest_HasData_ReturnsChatModel([NoAutoProperties] ChatModel model)
    {
        //Arrange
        var entity = _mapper.Map<ChatEntity>(model);
        _repoMock.Setup(repo => repo.Update(It.IsAny<ChatEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.Update(Guid.Empty, model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }
}