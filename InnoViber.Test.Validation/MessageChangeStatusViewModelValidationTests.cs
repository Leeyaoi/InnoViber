using InnoViber.API.Validators;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.Test.Validation;

public class MessageChangeStatusViewModelValidationTests
{
    private readonly MessageChangeStatusViewModelValidation _validator;

    public MessageChangeStatusViewModelValidationTests()
    {
        _validator = new MessageChangeStatusViewModelValidation();
    }

    [Fact]
    public void MessageStatusValidator_ShouldPass_WhenEverythingIsValid()
    {
        // Arrange
        var user = new MessageChangeStatusViewModel()
        {
            Status = 0
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.True(result.IsValid);
    }
}
