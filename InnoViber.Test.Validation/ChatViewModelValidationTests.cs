using InnoViber.API.Validators;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.Test.Validation;

public class ChatViewModelValidationTests
{
    private readonly ChatViewModelValidation _validator;

    public ChatViewModelValidationTests()
    {
        _validator = new ChatViewModelValidation();
    }

    [Fact]
    public void MessageStatusValidator_ShouldPass_WhenEverythingIsValid()
    {
        // Arrange
        var chat = new ChatShortViewModel()
        {
            Name = "123456"
        };

        // Act
        var result = _validator.Validate(chat);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void MessageStatusValidator_ShouldFail_WhenNameIsLong()
    {
        // Arrange
        var chat = new ChatShortViewModel()
        {
            Name = "12345678910"
        };

        // Act
        var result = _validator.Validate(chat);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void MessageStatusValidator_ShouldFail_WhenNameIsShort()
    {
        // Arrange
        var chat = new ChatShortViewModel()
        {
            Name = ""
        };

        // Act
        var result = _validator.Validate(chat);

        // Assert
        Assert.False(result.IsValid);
    }
}
