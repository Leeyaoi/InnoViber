using AutoFixture.Xunit2;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.ChatRole;
using InnoViber.API.ViewModels.Message;
using InnoViber.API.ViewModels.User;
using InnoViber.Test.Integration.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using System.Net.Http.Json;

namespace InnoViber.Test.Integration;

[Route("api/[controller]")]
[ApiController]
public class ChatRoleControllerTests : BaseTestClass
{
    public ChatRoleControllerTests(DataBaseWebApplicationFactory factory) : base(factory)
    { }

    [Theory, AutoData]
    public async Task PostRole_HasData_ReturnsOk(ChatRoleShortViewModel request)
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
    public async Task PutRole_HasData_ReturnsOk(ChatRoleShortViewModel request)
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
    public async Task GetRoles_HasData_ReturnsOk()
    {
        //Act
        var response = await _client.GetAsync("/api/ChatRole");

        // Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }

    [Theory, AutoData]
    public async Task DeleteRole_HasData_ReturnsOk(ChatRoleShortViewModel request)
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
