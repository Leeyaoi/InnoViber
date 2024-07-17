using InnoViber.API.Validators;
using InnoViber.API.ViewModels.Message;

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
            UserId = Guid.NewGuid().ToString(),
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
            UserId = "",
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
            UserId = string.Empty,
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
            UserId = "",
            ChatId = new()
        };

        // Act
        var result = _validator.Validate(message);

        // Assert
        Assert.False(result.IsValid);
    }
}
