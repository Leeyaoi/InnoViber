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

public class UserServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUserRepository> _repoMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
            cfg.AddExpressionMapping();
        });

        _mapper = config.CreateMapper();

        _repoMock = new Mock<IUserRepository>();

        _service = new UserService(_mapper, _repoMock.Object);
    }

    [Fact]
    public async Task GetAllUsersTest()
    {
        //Arrange

        var data = new Fixture().Create<List<UserEntity>>();

        _repoMock.Setup(repo => repo.GetAll(default)).ReturnsAsync(data);

        //Act

        List<UserModel> result = await _service.GetAll(default);

        //Assert

        var entity = _mapper.Map<List<UserEntity>>(result);
        entity.ShouldBeEquivalentTo(data);
    }

    [Fact]
    public async Task GetByIdTest()
    {
        //Arrange

        var data = new Fixture().Create<UserEntity>();

        _repoMock.Setup(repo => repo.GetById(Guid.Empty, default)).ReturnsAsync(data);

        //Act

        UserModel? result = await _service.GetById(Guid.Empty, default);

        //Assert

        var entity = _mapper.Map<UserEntity>(result);
        entity.ShouldBeEquivalentTo(data);
    }

    [Fact]
    public async Task GetByPredicateTest()
    {
        //Arrange

        var data = new Fixture().Create<UserEntity>();

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.Name == "Test", default)).ReturnsAsync(data);

        //Act

        UserModel? result = await _service.GetByPredicate(x => x.Name == "Test", default);

        //Assert

        var entity = _mapper.Map<UserEntity>(result);
        entity.ShouldBeEquivalentTo(data);
    }
}