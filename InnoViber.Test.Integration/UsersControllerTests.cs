using InnoViber.API.ViewModels.User;
using InnoViber.Test.Integration.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using System.Net.Http.Json;

namespace InnoViber.Test.Integration;

[Route("api/[controller]")]
[ApiController]
public class UsersControllerTests : BaseTestClass
{
    public UsersControllerTests(DataBaseWebApplicationFactory factory) : base(factory)
    {}

    [Fact]
    public async Task PostUser_HasData_ReturnsOk()
    {
        //Arrange
        var request = UserViewModels.ShortUser;

        //Act
        var response = await AddModelToDatabase<UserViewModel, UserShortViewModel>("/api/User", request);

        //Assert
        response.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetUsers_HasData_ReturnsOk()
    {
        //Act
        var response = await _client.GetAsync("/api/User");

        // Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }

    [Fact]
    public async Task DeleteUser_HasData_ReturnsOk()
    {
        //Arrange
        var request = UserViewModels.ShortUser;
        var user = await AddModelToDatabase<UserViewModel, UserShortViewModel>("/api/User", request);

        //Act
        var response = await _client.DeleteAsync($"/api/User/{user.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
        response.ShouldNotBeNull();
    }
}
