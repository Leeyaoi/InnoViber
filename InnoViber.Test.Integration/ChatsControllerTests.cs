using InnoViber.API.ViewModels.Chat;
using InnoViber.Test.Integration.Data;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System.Net.Http.Json;

namespace InnoViber.Test.Integration;

[Collection("Tests")]
public class ChatsControllerTests : BaseTestClass
{
    public ChatsControllerTests(DataBaseWebApplicationFactory factory) : base(factory)
    { }

    [Fact]
    public async Task PostChat_HasData_ReturnsOk()
    {
        // Arrange
        var request = ChatViewModels.ShortChat;

        //Act
        var response = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", request, default);

        //Assert
        response.ShouldNotBeNull();
    }

    [Fact]
    public async Task PutChat_HasData_ReturnsOk()
    {
        // Arrange
        var request = ChatViewModels.ShortChat;
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", request, default);

        //Act
        var response = await _client.PutAsJsonAsync($"/api/Chat/{chat.Id}", request, options: null, default);

        // Assert
        response.EnsureSuccessStatusCode();
        var userResponse = await response.Content.ReadFromJsonAsync<ChatViewModel>(default);

        userResponse.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetChat_HasData_ReturnsOk()
    {
        //Act
        var response = await _client.GetAsync("/api/Chat");

        // Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }

    [Fact]
    public async Task DeleteChat_HasData_ReturnsOk()
    {
        //Arrange
        var request = ChatViewModels.ShortChat;
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", request, default);

        //Act
        var response = await _client.DeleteAsync($"/api/Chat/{chat.Id}", default);

        //Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }
}
