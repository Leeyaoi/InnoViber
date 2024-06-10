using AutoFixture;
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

    [Fact]
    public async Task GetAllChatsTest_HasData_List()
    {
        //Arrange

        var data = new Fixture().Create<List<ChatEntity>>();

        _repoMock.Setup(repo => repo.GetAll(default)).ReturnsAsync(data);

        //Act

        List<ChatModel> result = await _service.GetAll(default);

        //Assert

        var entity = _mapper.Map<List<ChatEntity>>(result);
        entity.ShouldBeEquivalentTo(data);
    }

    [Fact]
    public async Task GetByIdTest_HasData_Model()
    {
        //Arrange

        var data = new Fixture().Create<ChatEntity>();

        _repoMock.Setup(repo => repo.GetById(Guid.Empty, default)).ReturnsAsync(data);

        //Act

        ChatModel? result = await _service.GetById(Guid.Empty, default);

        //Assert

        var entity = _mapper.Map<ChatEntity>(result);
        entity.ShouldBeEquivalentTo(data);
    }

    [Fact]
    public async Task GetByPredicateTest_HasData_Model()
    {
        //Arrange


        var data = new Fixture().Create<ChatEntity>();

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.Name == "Test1", default)).ReturnsAsync(data);

        //Act

        ChatModel? result = await _service.GetByPredicate(x => x.Name == "Test1", default);

        //Assert

        var entity = _mapper.Map<ChatEntity>(result);
        entity.ShouldBeEquivalentTo(data);
    }
}