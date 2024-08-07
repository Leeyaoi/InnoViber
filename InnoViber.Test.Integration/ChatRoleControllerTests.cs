﻿using AutoFixture.Xunit2;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.ChatRole;
using InnoViber.API.ViewModels.Message;
using InnoViber.Test.Integration.Data;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System.Net.Http.Json;
using System.Text.Json;

namespace InnoViber.Test.Integration;

[Collection("Tests")]
public class ChatRoleControllerTests : BaseTestClass
{
    public ChatRoleControllerTests(DataBaseWebApplicationFactory factory) : base(factory)
    { }

    [Theory, AutoData]
    public async Task PostUser_HasData_ReturnsOk(ChatRoleShortViewModel request)
    {
        //Arrange
        var chatVM = ChatViewModels.ShortChat;
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM, default);
        request.UserId = Guid.NewGuid().ToString();
        request.ChatId = chat.Id;

        //Act
        var response = await AddModelToDatabase<ChatRoleViewModel, ChatRoleShortViewModel>("/api/ChatRole", request, default);

        //Assert
        response.ShouldNotBeNull();
    }

    [Theory, AutoData]
    public async Task PutUser_HasData_ReturnsOk(ChatRoleShortViewModel request)
    {
        //Arrange
        var chatVM = ChatViewModels.ShortChat;
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM, default);
        request.UserId = Guid.NewGuid().ToString();
        request.ChatId = chat.Id;

        var role = await AddModelToDatabase<ChatRoleViewModel, ChatRoleShortViewModel>("/api/ChatRole", request, default);

        //Act
        var response = await _client.PutAsJsonAsync($"/api/ChatRole/{role.Id}", request, options: null, default);

        // Assert
        response.EnsureSuccessStatusCode();
        var userResponse = await response.Content.ReadFromJsonAsync<MessageViewModel>(default);

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
        var chatVM = ChatViewModels.ShortChat;
        var chat = await AddModelToDatabase<ChatViewModel, ChatShortViewModel>("/api/Chat", chatVM, default);
        request.UserId = Guid.NewGuid().ToString();
        request.ChatId = chat.Id;

        var role = await AddModelToDatabase<ChatRoleViewModel, ChatRoleShortViewModel>("/api/ChatRole", request, default);

        //Act
        var response = await _client.DeleteAsync($"/api/ChatRole/{role.Id}", default);

        //Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }
}
