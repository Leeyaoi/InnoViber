using InnoViber.API.Validators;
using InnoViber.API.ViewModels.ChatRole;

namespace InnoViber.Test.Validation;

public class ChatRoleViewModelValidationTests
{
    private readonly ChatRoleViewModelValidation _validator;

    public ChatRoleViewModelValidationTests()
    {
        _validator = new ChatRoleViewModelValidation();
    }

    [Fact]
    public void ChatRoleValidator_ShouldPass_WhenEverythingIsValid()
    {
        // Arrange
        var ChatRole = new ChatRoleViewModel()
        {
            Role = 0,
            UserId = Guid.NewGuid().ToString(),
            ChatId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(ChatRole);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void ChatRoleValidator_ShouldFail_WhenUserIdIsEmpty()
    {
        // Arrange
        var ChatRole = new ChatRoleViewModel()
        {
            Role = 0,
            UserId = string.Empty,
            ChatId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(ChatRole);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void ChatRoleValidator_ShouldFail_WhenChatIdIsEmpty()
    {
        // Arrange
        var ChatRole = new ChatRoleViewModel()
        {
            Role = 0,
            UserId = "",
            ChatId = new()
        };

        // Act
        var result = _validator.Validate(ChatRole);

        // Assert
        Assert.False(result.IsValid);
    }
}