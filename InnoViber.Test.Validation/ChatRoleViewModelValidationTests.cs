using InnoViber.API.Validators;
using InnoViber.API.ViewModels.ChatRole;
using InnoViber.API.ViewModels.User;

namespace InnoViber.Test.Validation;

public class ChatRoleViewModelValidationTests
{
    private readonly ChatRoleViewModelValidation _validator;

    public ChatRoleViewModelValidationTests()
    {
        _validator = new ChatRoleViewModelValidation();
    }

    [Fact]
    public void UserValidator_ShouldPass_WhenEverythingIsValid()
    {
        // Arrange
        var user = new ChatRoleViewModel()
        {
            Role = 0,
            UserId = Guid.NewGuid(),
            ChatId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void UserValidator_ShouldFail_WhenUserIdIsEmpty()
    {
        // Arrange
        var user = new ChatRoleViewModel()
        {
            Role = 0,
            UserId = new(),
            ChatId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void UserValidator_ShouldFail_WhenChatIdIsEmpty()
    {
        // Arrange
        var user = new ChatRoleViewModel()
        {
            Role = 0,
            UserId = Guid.NewGuid(),
            ChatId = new()
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.False(result.IsValid);
    }
}