using AutoFixture.Xunit2;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.ChatRole;
using InnoViber.API.ViewModels.Message;
using InnoViber.API.ViewModels.User;
using InnoViber.Test.Integration.Data;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System.Data;
using System.Net.Http.Json;

namespace InnoViber.Test.Integration;

public class ChatRoleControllerTests : BaseTestClass
{
    public ChatRoleControllerTests(DataBaseWebApplicationFactory<Program> factory) : base(factory)
    { }

    [Theory, AutoData]
    public async Task PostUser_HasData_ReturnsOk(ChatRoleShortViewModel request)
    {
        //Arrange
        var userVM = UserViewModels.ShortUser;
        var chatVM = ChatViewModels.ShortChat;
        var user = await AddModelToDatabase<UserViewModel, UserShortViewModel>("/api/User", userVM);
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM);
        request.UserId = user.Id;
        request.ChatId = chat.Id;

        //Act
        var response = await AddModelToDatabase<ChatRoleViewModel, ChatRoleShortViewModel>("/api/ChatRole", request);

        //Assert
        response.ShouldNotBeNull();
    }

    [Theory, AutoData]
    public async Task PutUser_HasData_ReturnsOk(ChatRoleShortViewModel request)
    {
        //Arrange
        var userVM = UserViewModels.ShortUser;
        var chatVM = ChatViewModels.ShortChat;
        var user = await AddModelToDatabase<UserViewModel, UserShortViewModel>("/api/User", userVM);
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM);
        request.UserId = user.Id;
        request.ChatId = chat.Id;

        var role = await AddModelToDatabase<ChatRoleViewModel, ChatRoleShortViewModel>("/api/ChatRole", request);

        //Act
        var response = await _client.PutAsJsonAsync($"/api/ChatRole/{role.Id}", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var userResponse = await response.Content.ReadFromJsonAsync<MessageViewModel>();

        userResponse.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetUsers_HasData_ReturnsOk()
    {
        //Act
        var response = await _client.GetAsync("/api/ChatRole");

        // Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }

    [Theory, AutoData]
    public async Task DeleteUser_HasData_ReturnsOk(ChatRoleShortViewModel request)
    {
        //Arrange
        var userVM = UserViewModels.ShortUser;
        var chatVM = ChatViewModels.ShortChat;
        var user = await AddModelToDatabase<UserViewModel, UserShortViewModel>("/api/User", userVM);
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM);
        request.UserId = user.Id;
        request.ChatId = chat.Id;

        var role = await AddModelToDatabase<ChatRoleViewModel, ChatRoleShortViewModel>("/api/ChatRole", request);

        //Act
        var response = await _client.DeleteAsync($"/api/ChatRole/{role.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }
}
