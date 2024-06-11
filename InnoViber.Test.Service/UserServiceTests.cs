using AutoFixture;
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

        _repoMock.Setup(repo => repo.GetByPredicate(x => x.Name == "Test", default)).ReturnsAsync(user);

        //Act

        UserModel? result = await _service.GetByPredicate(x => x.Name == "Test", default);

        //Assert

        var entity = _mapper.Map<UserEntity>(result);
        entity.ShouldBeEquivalentTo(user);
    }
}