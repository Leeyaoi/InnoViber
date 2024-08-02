using AutoFixture.Xunit2;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using InnoViber.BLL.Helpers;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using InnoViber.Domain.Enums;
using InnoViber.Domain.Providers;
using Moq;
using Shouldly;

namespace InnoViber.Test.Service;

public class MessageServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IMessageRepository> _repoMock;
    private readonly Mock<IDateTimeProvider> _dateMock;
    private readonly MessageService _service;

    public MessageServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BllLayerMapperProfile());
            cfg.AddExpressionMapping();
        });

        _mapper = config.CreateMapper();

        _repoMock = new Mock<IMessageRepository>();

        _dateMock = new Mock<IDateTimeProvider>();

        _service = new MessageService(_mapper, _repoMock.Object, _dateMock.Object);
    }

    [Theory, AutoData]
    public async Task GetAllMessagesTest_HasData_ReturnsMessages([NoAutoProperties] List<MessageEntity> messages)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetAll(default)).ReturnsAsync(messages);

        //Act

        var result = await _service.GetAll(default);

        //Assert

        var entity = _mapper.Map<List<MessageEntity>>(result);
        entity.ShouldBeEquivalentTo(messages);
    }

    [Theory, AutoData]
    public async Task GetByIdTest_HasData_ReturnsMessage([NoAutoProperties] MessageEntity message)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetById(Guid.Empty, default)).ReturnsAsync(message);

        //Act

        var result = await _service.GetById(Guid.Empty, default);

        //Assert

        var entity = _mapper.Map<MessageEntity>(result);
        entity.ShouldBeEquivalentTo(message);
    }

    [Theory, AutoData]
    public async Task GetByPredicateTest_HasData_ReturnsMessage([NoAutoProperties] List<MessageEntity> message)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.Status == MessageStatus.Delivered, default)).ReturnsAsync(message);

        //Act

        var result = await _service.GetByPredicate(x => x.Status == MessageStatus.Delivered, default);

        //Assert

        var entity = _mapper.Map<List<MessageEntity>>(result);
        entity.ShouldBeEquivalentTo(message);
    }

    [Theory, AutoData]
    public async Task CreateTest_HasData_ReturnsMessageModel([NoAutoProperties] MessageModel model)
    {
        //Arrange
        var entity = _mapper.Map<MessageEntity>(model);
        _repoMock.Setup(repo => repo.Create(It.IsAny<MessageEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.Create(model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }

    [Theory, AutoData]
    public async Task UpdateTest_HasData_ReturnsMessageModel([NoAutoProperties] MessageModel model)
    {
        //Arrange
        var entity = _mapper.Map<MessageEntity>(model);
        _repoMock.Setup(repo => repo.Update(It.IsAny<MessageEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.Update(Guid.Empty, model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }
}