using AutoMapper.Extensions.ExpressionMapping;
using InnoViber.BLL.Helpers;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Moq;
using Shouldly;
using System.Linq;
using System.Linq.Expressions;

namespace InnoViber.Test.Service;

public class UserServiceTests
{
    [Fact]
    public async Task GetAllUsersTest()
    {
        //Arrange

        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
        });

        var mapper = config.CreateMapper();

        var mock = new Mock<IUserRepository>();
        mock.Setup(repo => repo.GetAll(CancellationToken.None)).Returns( () => Task.FromResult(GetTestUsers()));

        var service = new UserService(mapper, mock.Object);

        //Act

        List<UserModel> result = await service.GetAll(CancellationToken.None);

        //Assert

        var entity = mapper.Map<List<UserEntity>>(result);
        entity.ShouldBeEquivalentTo(GetTestUsers());
    }

    [Fact]
    public async Task GetByIdTest()
    {
        //Arrange

        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
        });

        var mapper = config.CreateMapper();

        var mock = new Mock<IUserRepository>();
        mock.Setup(repo => repo.GetById(Guid.Empty, CancellationToken.None)).Returns(() => Task.FromResult(GetOne()));

        var service = new UserService(mapper, mock.Object);

        //Act

        UserModel? result = await service.GetById(Guid.Empty, CancellationToken.None);

        //Assert

        var entity = mapper.Map<UserEntity>(result);
        entity.ShouldBeEquivalentTo(GetOne());
    }

    [Fact]
    public async Task GetByPredicateTest()
    {
        //Arrange

        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
            cfg.AddExpressionMapping();
        });

        var mapper = config.CreateMapper();

        var mock = new Mock<IUserRepository>();
        mock.Setup(repo => repo.GetByPredicate(x => x.Name == "Test", CancellationToken.None))
            .Returns(() => Task.FromResult(GetByPred(x => x.Name == "Test")));

        var service = new UserService(mapper, mock.Object);

        //Act

        UserModel? result = await service.GetByPredicate(x => x.Name == "Test", CancellationToken.None);

        //Assert

        var entity = mapper.Map<UserEntity>(result);
        entity.ShouldBeEquivalentTo(GetByPred(x => x.Name == "Test"));
    }

    private List<UserEntity> GetTestUsers()
    {
        var users = new List<UserEntity>
            {
                new UserEntity { Name="Tom", Surname = "Test", Email = "Test"},
                new UserEntity { Name="Alice", Surname = "Test", Email = "Test"},
                new UserEntity { Name="Sam", Surname = "Test", Email = "Test"},
                new UserEntity { Name="Kate", Surname = "Test", Email = "Test"}
            };
        return users;
    }

    private UserEntity GetOne()
    {
        return new UserEntity()
        {
            Name = "Test",
            Surname = "Test",
            Email = "Test"
        };
    }

    private UserEntity GetByPred(Predicate<UserEntity> exp)
    {
        List<UserEntity> users = new List<UserEntity>
            {
                new UserEntity { Name="Test", Surname = "Test", Email = "Test"},
                new UserEntity { Name="Alice", Surname = "Test", Email = "Test"},
                new UserEntity { Name="Sam", Surname = "Test", Email = "Test"},
                new UserEntity { Name="Kate", Surname = "Test", Email = "Test"}
            };
        return users.Find(exp)!;
    }
}