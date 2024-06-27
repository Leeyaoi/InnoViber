using FluentValidation;
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
        var user = new UserViewModel()
        {
            Id = Guid.NewGuid(),
            MongoId = Guid.NewGuid(),
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void UserValidator_ShouldFail_WhenMongoIdIsEmpty()
    {
        // Arrange
        var user = new UserViewModel()
        {
            Id = Guid.NewGuid(),
            MongoId = new(),
        };

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.False(result.IsValid);
    }
}
