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

public class MessageServiceTests
{
    [Fact]
    public async Task GetAllMessagesTest()
    {
        //Arrange
        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
        });

        var mapper = config.CreateMapper();

        var mock = new Mock<IMessageRepository>();
        mock.Setup(repo => repo.GetAll(CancellationToken.None)).Returns(() => Task.FromResult(GetTestMessages()));

        var service = new MessageService(mapper, mock.Object);

        //Act

        List<MessageModel> result = await service.GetAll(CancellationToken.None);

        //Assert

        var entity = mapper.Map<List<MessageEntity>>(result);
        entity.ShouldBeEquivalentTo(GetTestMessages());
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

        var mock = new Mock<IMessageRepository>();
        mock.Setup(repo => repo.GetById(Guid.Empty, CancellationToken.None)).Returns(() => Task.FromResult(GetOne()));

        var service = new MessageService(mapper, mock.Object);

        //Act

        MessageModel? result = await service.GetById(Guid.Empty, CancellationToken.None);

        //Assert

        var entity = mapper.Map<MessageEntity>(result);
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

        var mock = new Mock<IMessageRepository>();
        mock.Setup(repo => repo.GetByPredicate(x => x.Status == 1, CancellationToken.None))
            .Returns(() => Task.FromResult(GetByPred(x => x.Status == 1)));

        var service = new MessageService(mapper, mock.Object);

        //Act

        MessageModel? result = await service.GetByPredicate(x => x.Status == 1, CancellationToken.None);

        //Assert

        var entity = mapper.Map<MessageEntity>(result);
        entity.ShouldBeEquivalentTo(GetByPred(x => x.Status == 1));
    }

    private List<MessageEntity> GetTestMessages()
    {
        var messages = new List<MessageEntity>
            {
                new MessageEntity { Status = 1, Text = "Test1"},
                new MessageEntity { Status = 2, Text = "Test2"},
                new MessageEntity { Status = 3, Text = "Test3"}
            };
        return messages;
    }

    private MessageEntity GetOne()
    {
        return new MessageEntity()
        {
            Status = 1,
            Text = "Test"
        };
    }

    private MessageEntity GetByPred(Predicate<MessageEntity> exp)
    {
        var messages = new List<MessageEntity>
            {
                new MessageEntity { Status = 1, Text = "Test1"},
                new MessageEntity { Status = 2, Text = "Test2"},
                new MessageEntity { Status = 3, Text = "Test3"}
            };
        return messages.Find(exp)!;
    }
}