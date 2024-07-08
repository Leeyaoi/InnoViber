using InnoViber.API.Validators;
using InnoViber.API.ViewModels.Message;
using InnoViber.API.ViewModels.User;

namespace InnoViber.Test.Validation;

public class MessageViewModelValidationTests
{
    private readonly MessageViewModelValidation _validator;

    public MessageViewModelValidationTests()
    {
        _validator = new MessageViewModelValidation();
    }

    [Fact]
    public void MessageValidator_ShouldPass_WhenEverythingIsValid()
    {
        // Arrange
        var message = new MessageShortViewModel()
        {
            Text = "Test",
            UserId = Guid.NewGuid(),
            ChatId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(message);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void MessageValidator_ShouldFail_WhenTextIsEmpty()
    {
        // Arrange
        var message = new MessageShortViewModel()
        {
            Text = "",
            UserId = Guid.NewGuid(),
            ChatId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(message);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void MessageValidator_ShouldFail_WhenUserIdIsEmpty()
    {
        // Arrange
        var message = new MessageShortViewModel()
        {
            Text = "Test",
            UserId = new(),
            ChatId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(message);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void MessageValidator_ShouldFail_WhenChatIdIsEmpty()
    {
        // Arrange
        var message = new MessageShortViewModel()
        {
            Text = "Test",
            UserId = Guid.NewGuid(),
            ChatId = new()
        };

        // Act
        var result = _validator.Validate(message);

        // Assert
        Assert.False(result.IsValid);
    }
}
