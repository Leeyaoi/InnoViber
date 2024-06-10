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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InnoViber.Test.Service;

public class MessageServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IMessageRepository> _repoMock;
    private readonly MessageService _service;

    public MessageServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
            cfg.AddExpressionMapping();
        });

        _mapper = config.CreateMapper();

        _repoMock = new Mock<IMessageRepository>();

        _service = new MessageService(_mapper, _repoMock.Object);
    }

    [Fact]
    public async Task GetAllMessagesTest_HasData_List()
    {
        //Arrange

        var data = new Fixture().Create<List<MessageEntity>>();

        _repoMock.Setup(repo => repo.GetAll(default)).ReturnsAsync(data);

        //Act

        List<MessageModel> result = await _service.GetAll(default);

        //Assert

        var entity = _mapper.Map<List<MessageEntity>>(result);
        entity.ShouldBeEquivalentTo(data);
    }

    [Fact]
    public async Task GetByIdTest_HasData_Model()
    {
        //Arrange

        var data = new Fixture().Create<MessageEntity>();

        _repoMock.Setup(repo => repo.GetById(Guid.Empty, default)).ReturnsAsync(data);

        //Act

        MessageModel? result = await _service.GetById(Guid.Empty, default);

        //Assert

        var entity = _mapper.Map<MessageEntity>(result);
        entity.ShouldBeEquivalentTo(data);
    }

    [Fact]
    public async Task GetByPredicateTest_HasData_Model()
    {
        //Arrange

        var data = new Fixture().Create<MessageEntity>();

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.Status == 1, default)).ReturnsAsync(data);

        //Act

        MessageModel? result = await _service.GetByPredicate(x => x.Status == 1, default);

        //Assert

        var entity = _mapper.Map<MessageEntity>(result);
        entity.ShouldBeEquivalentTo(data);
    }
}