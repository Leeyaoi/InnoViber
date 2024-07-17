using AutoFixture.Xunit2;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.Message;
using InnoViber.Test.Integration.Data;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System.Net.Http.Json;

namespace InnoViber.Test.Integration;

[Collection("Tests")]
public class MessagesControllerTests : BaseTestClass
{
    public MessagesControllerTests(DataBaseWebApplicationFactory factory) : base(factory)
    { }

    [Theory, AutoData]
    public async Task PostMessage_HasData_ReturnsOk(MessageShortViewModel request)
    {
        //Arrange
        var chatVM = ChatViewModels.ShortChat;
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM);
        request.UserId = Guid.NewGuid().ToString();
        request.ChatId = chat.Id;

        //Act
        var response = await AddModelToDatabase<MessageViewModel, MessageShortViewModel>("/api/Message", request);

        //Assert
        response.ShouldNotBeNull();
    }

    [Theory, AutoData]
    public async Task PutMessage_HasData_ReturnsOk(MessageShortViewModel request)
    {
        //Arrange
        var chatVM = ChatViewModels.ShortChat;
        Console.WriteLine(chatVM.ToString());
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM);
        request.UserId = Guid.NewGuid().ToString();
        request.ChatId = chat.Id;

        Console.WriteLine(request.ToString());
        var message = await AddModelToDatabase<MessageViewModel, MessageShortViewModel>("/api/Message", request);
        Console.WriteLine(message.ToString());

        //Act
        var response = await _client.PutAsJsonAsync($"/api/Message/{message.Id}", request);
        Console.WriteLine(response.ToString());

        // Assert
        response.EnsureSuccessStatusCode();
        var userResponse = await response.Content.ReadFromJsonAsync<MessageViewModel>();
        Console.WriteLine(userResponse.ToString());

        userResponse.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetMessages_HasData_ReturnsOk()
    {
        //Act
        var response = await _client.GetAsync("/api/Message");

        // Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }

    [Theory, AutoData]
    public async Task DeleteMessage_HasData_ReturnsOk(MessageShortViewModel request)
    {
        //Arrange
        var chatVM = ChatViewModels.ShortChat;
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM);
        request.UserId = Guid.NewGuid().ToString();
        request.ChatId = chat.Id;

        var message = await AddModelToDatabase<MessageViewModel, MessageShortViewModel>("/api/Message", request);

        //Act
        var response = await _client.DeleteAsync($"/api/Message/{message.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }
}
