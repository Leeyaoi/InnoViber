using InnoViber.API.Validators;
using InnoViber.API.ViewModels.User;

namespace InnoViber.Test.Validation;

public class UserViewModelValidationTests
{
    private readonly UserViewModelValidation _validator;

    public UserViewModelValidationTests()
    {
        _validator = new UserViewModelValidation();
    }

    [Fact]
    public void UserValidator_ShouldPass_WhenEverythingIsValid()
    {
        // Arrange
        var user = new UserShortViewModel()
        {
            MongoId = Guid.NewGuid(),
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void UserValidator_ShouldFail_WhenEmailIsWrong()
    {
        // Arrange
        var user = new UserShortViewModel()
        {
            MongoId = Guid.NewGuid(),
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void UserValidator_ShouldFail_WhenNameIsWrong()
    {
        // Arrange
        var user = new UserShortViewModel()
        {
            MongoId = Guid.NewGuid(),
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void UserValidator_ShouldFail_WhenSurnameIsWrong()
    {
        // Arrange
        var user = new UserShortViewModel()
        {
            MongoId = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.False(result.IsValid);
    }
}
