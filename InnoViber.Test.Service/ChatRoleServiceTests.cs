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

public class ChatRoleServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IChatRoleRepository> _repoMock;
    private readonly ChatRoleService _service;

    public ChatRoleServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BllLayerMapperProfile());
            cfg.AddExpressionMapping();
        });

        _mapper = config.CreateMapper();

        _repoMock = new Mock<IChatRoleRepository>();

        _service = new ChatRoleService(_mapper, _repoMock.Object);
    }

    [Theory, AutoData]
    public async Task GetAllMessagesTest_HasData_ReturnsRoles([NoAutoProperties] List<ChatRoleEntity> messages)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetAll(default)).ReturnsAsync(messages);

        //Act

        var result = await _service.GetAll(default);

        //Assert

        var entity = _mapper.Map<List<ChatRoleEntity>>(result);
        entity.ShouldBeEquivalentTo(messages);
    }

    [Theory, AutoData]
    public async Task GetByIdTest_HasData_ReturnsMessage([NoAutoProperties] ChatRoleEntity message)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetById(Guid.Empty, default)).ReturnsAsync(message);

        //Act

        var result = await _service.GetById(Guid.Empty, default);

        //Assert

        var entity = _mapper.Map<ChatRoleEntity>(result);
        entity.ShouldBeEquivalentTo(message);
    }

    [Theory, AutoData]
    public async Task GetByPredicateTest_HasData_ReturnsMessage([NoAutoProperties] ChatRoleEntity message)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.Role == UserRoles.Owner, default)).ReturnsAsync(message);

        //Act

        var result = await _service.GetByPredicate(x => x.Role == UserRoles.Owner, default);

        //Assert

        var entity = _mapper.Map<ChatRoleEntity>(result);
        entity.ShouldBeEquivalentTo(message);
    }

    [Theory, AutoData]
    public async Task CreateTest_HasData_ReturnsMessageModel([NoAutoProperties] ChatRoleModel model)
    {
        //Arrange
        var entity = _mapper.Map<ChatRoleEntity>(model);
        _repoMock.Setup(repo => repo.Create(It.IsAny<ChatRoleEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.Create(model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }

    [Theory, AutoData]
    public async Task UpdateTest_HasData_ReturnsMessageModel([NoAutoProperties] ChatRoleModel model)
    {
        //Arrange
        var entity = _mapper.Map<ChatRoleEntity>(model);
        _repoMock.Setup(repo => repo.Update(It.IsAny<ChatRoleEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.Update(Guid.Empty, model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }

    [Theory, AutoData]
    public async Task UpdateStatusTest_HasData_ReturnsMessageModel([NoAutoProperties] ChatRoleModel model)
    {
        //Arrange
        var entity = _mapper.Map<ChatRoleEntity>(model);
        entity.Role = UserRoles.Owner;
        _repoMock.Setup(repo => repo.Update(It.IsAny<ChatRoleEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.UpdateRole(UserRoles.Owner, model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }
}