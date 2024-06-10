using AutoMapper.Extensions.ExpressionMapping;
using InnoViber.BLL.Helpers;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Moq;
using Shouldly;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace InnoViber.Test.Service;

public class ChatServiceTests
{
    [Fact]
    public async Task GetAllChatsTest()
    {
        //Arrange

        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
        });

        var mapper = config.CreateMapper();

        var mock = new Mock<IChatRepository>();
        mock.Setup(repo => repo.GetAll(CancellationToken.None)).Returns(() => Task.FromResult(GetTestChats()));

        var service = new ChatService(mapper, mock.Object);

        //Act

        List<ChatModel> result = await service.GetAll(CancellationToken.None);

        //Assert

        var entity = mapper.Map<List<ChatEntity>>(result);
        entity.ShouldBeEquivalentTo(GetTestChats());
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

        var mock = new Mock<IChatRepository>();
        mock.Setup(repo => repo.GetById(Guid.Empty, CancellationToken.None)).Returns(() => Task.FromResult(GetOne()));

        var service = new ChatService(mapper, mock.Object);

        //Act

        ChatModel? result = await service.GetById(Guid.Empty, CancellationToken.None);

        //Assert

        var entity = mapper.Map<ChatEntity>(result);
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

        var mock = new Mock<IChatRepository>();
        mock.Setup(repo => repo.GetByPredicate(x => x.Name == "Test1", CancellationToken.None))
            .Returns(() => Task.FromResult(GetByPred(x => x.Name == "Test1")));

        var service = new ChatService(mapper, mock.Object);

        //Act

        ChatModel? result = await service.GetByPredicate(x => x.Name == "Test1", CancellationToken.None);

        //Assert

        var entity = mapper.Map<ChatEntity>(result);
        entity.ShouldBeEquivalentTo(GetByPred(x => x.Name == "Test1"));
    }

    private List<ChatEntity> GetTestChats()
    {
        var chats = new List<ChatEntity>
            {
                new ChatEntity { Name = "Test1"},
                new ChatEntity { Name = "Test2"},
                new ChatEntity { Name = "Test3"}
            };
        return chats;
    }

    private ChatEntity GetOne()
    {
        return new ChatEntity()
        {
            Name = "Test"
        };
    }

    private ChatEntity GetByPred(Predicate<ChatEntity> exp)
    {
        var chats = new List<ChatEntity>
            {
                new ChatEntity { Name = "Test1"},
                new ChatEntity { Name = "Test2"},
                new ChatEntity { Name = "Test3"}
            };
        return chats.Find(exp)!;
    }
}