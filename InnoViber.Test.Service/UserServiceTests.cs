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

public class UserServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUserRepository> _repoMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BllLayerMapperProfile());
            cfg.AddExpressionMapping();
        });

        _mapper = config.CreateMapper();

        _repoMock = new Mock<IUserRepository>();

        _service = new UserService(_mapper, _repoMock.Object);
    }

    [Theory, AutoData]
    public async Task GetAllUsersTest_HasData_ReturnsUsers([NoAutoProperties] List<UserEntity> users)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetAll(default)).ReturnsAsync(users);

        //Act

        var result = await _service.GetAll(default);

        //Assert

        var entity = _mapper.Map<List<UserEntity>>(result);
        entity.ShouldBeEquivalentTo(users);
    }

    [Theory, AutoData]
    public async Task GetByIdTest_HasData_ReturnsUser([NoAutoProperties] UserEntity user)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetById(Guid.Empty, default)).ReturnsAsync(user);

        //Act

        var result = await _service.GetById(Guid.Empty, default);

        //Assert

        var entity = _mapper.Map<UserEntity>(result);
        entity.ShouldBeEquivalentTo(user);
    }

    [Theory, AutoData]
    public async Task GetByPredicateTest_HasData_ReturnsUser([NoAutoProperties] UserEntity user)
    {
        //Arrange

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.MongoId == Guid.NewGuid(), default)).ReturnsAsync(user);

        //Act

        UserModel? result = await _service.GetByPredicate(x => x.MongoId == Guid.NewGuid(), default);

        //Assert

        var entity = _mapper.Map<UserEntity>(result);
        entity.ShouldBeEquivalentTo(user);
    }

    [Theory, AutoData]
    public async Task CreateTest_HasData_ReturnsMessageModel([NoAutoProperties] UserModel model)
    {
        //Arrange
        var entity = _mapper.Map<UserEntity>(model);
        _repoMock.Setup(repo => repo.Create(It.IsAny<UserEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.Create(model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }

    [Theory, AutoData]
    public async Task UpdateTest_HasData_ReturnsMessageModel([NoAutoProperties] UserModel model)
    {
        //Arrange
        var entity = _mapper.Map<UserEntity>(model);
        _repoMock.Setup(repo => repo.Update(It.IsAny<UserEntity>(), default)).ReturnsAsync(entity);

        //Act

        var result = await _service.Update(Guid.Empty, model, default);

        //Assert

        result.ShouldBeEquivalentTo(model);
    }
}